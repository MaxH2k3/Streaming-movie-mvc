using AutoMapper;
using Microsoft.Extensions.Configuration;
using SMovie.Application.Extension;
using SMovie.Application.Helper;
using SMovie.Application.IService;
using SMovie.Application.MessageService;
using SMovie.Domain.Constant;
using SMovie.Domain.Constants;
using SMovie.Domain.Entity;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;
using SMovie.Domain.UnitOfWork;
using SMovie.Infrastructure.UnitOfWork;
using System.Net;

namespace SMovie.Application.Service
{
	public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMailService mailService,
            IAuthenticationService authenticationService, IConfiguration configuration,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
            _authenticationService = authenticationService;
            _configuration = configuration;
            _mapper = mapper;
        }

        public UserService(IMailService mailService, IAuthenticationService authenticationService,
            IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork();
            _mailService = mailService;
            _authenticationService = authenticationService;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<User?> GetUser(Guid userId)
        {
            return await _unitOfWork.UserRepository.GetById(userId);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _unitOfWork.UserRepository.GetAll();
        }

        public async Task<ResponseDTO> Login(UserDTO userDTO)
        {
            var user = await _unitOfWork.UserRepository.GetUser(userDTO.UserName!);
            if (user == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, MessageUser.UserNameOrEmailNotExisted);
            }
            if (!_authenticationService.VerifyPasswordHash(userDTO.Password, user.Password!, user.PasswordSalt!))
            {
                return new ResponseDTO(HttpStatusCode.BadRequest, MessageUser.WrongPassword);
            }
            if (user.Status!.Equals(StatusAccount.BLOCK))
            {
                return new ResponseDTO(HttpStatusCode.Forbidden, MessageUser.UserBlocked);
            }

            return new ResponseDTO(HttpStatusCode.OK, MessageUser.LoginSuccessfully, user);
        }

        public async Task<ResponseDTO> Register(RegisterUser registerUser)
        {
            if (await _unitOfWork.UserRepository.IsExisted(UserFieldType.Username, registerUser.Username))
            {
                return new ResponseDTO(HttpStatusCode.Conflict, MessageUser.UserNameExisted);
            } else if (await _unitOfWork.UserRepository.IsExisted(UserFieldType.Email, registerUser.Email))
            {
                return new ResponseDTO(HttpStatusCode.Conflict, MessageUser.EmailExisted);
            }

            _authenticationService.CreatePasswordHash(registerUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

            //create temporary user
            Guid id = Guid.NewGuid();
            await _unitOfWork.UserTemporaryRepository.Add(new UserTemporary()
            {
                Email = registerUser.Email,
                Role = Role.RoleUser,
                MID = id,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                Username = registerUser.Username,
                DisplayName = registerUser.FirstName + " " + registerUser.LastName,
                Avatar = $"{UserCommon.PrePathUserAvatar}avatar{Utilities.RandomNumber(1, 4)}.jpg",
                Status = StatusAccount.PENDING,
                ExpiredDate = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["RegisterData:ExpireTimeForStorage"]!))
            });
            
            //create token verify
            string token = _authenticationService.CreateRandomToken();
            int code = new Random().Next(1000, 10000);
            await _unitOfWork.VerifyTokenRepository.Add(new VerifyToken()
            {
				MID = id,
                Token = token,
                Code = code,
                CreatedDate = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["RegisterData:ExpireTimeForVerification"]!)),
                ExpiredDate = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["RegisterData:ExpireTimeForStorage"]!))
            });

            //send mail
            var result = await _mailService.SendUsingTemplateFromFile(TemplateMail.VERIFY_CODE_MAIL, MessageMail.TitleVerifyMail, new UserMail()
            {
                UserName = registerUser.Username,
                UserId = id.ToString(),
                Token = token,
                Email = registerUser.Email,
                Code = code
            });

            if (result)
            {
                return new ResponseDTO(HttpStatusCode.Created, MessageMail.SendMailRegisterSuccessfully, id);
            }

            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, MessageMail.SendMailRegisterFailed);
        }

        public Task<ResponseDTO> ResendToken(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> VerifyAccount(string token, Guid userId, VerifyType type)
        {
			if (VerifyType.Code == type)
			{
				return await VerifyCode(Int32.Parse(token), userId);
			} else if (VerifyType.Token == type)
			{
				return await VerifyToken(token, userId);
			}

            throw new NotFoundException(MessageException.TypeNotFound);
		}

		private async Task<ResponseDTO> VerifyCode(int code, Guid userId)
		{
			var verifyCode = await _unitOfWork.VerifyTokenRepository.GetById(userId);
			if (verifyCode == null)
			{
				return new ResponseDTO(HttpStatusCode.NotFound, MessageUser.NotFoundAccountToVerify);
			} else if (verifyCode.Code != code)
			{
				return new ResponseDTO(HttpStatusCode.BadRequest, MessageUser.FailToVerifyCode);
			} else if (verifyCode.CreatedDate < DateTime.UtcNow)
			{
				return new ResponseDTO(HttpStatusCode.BadRequest, MessageUser.CodeExpired);
			}
			UserTemporary? userTemporary = await _unitOfWork.UserTemporaryRepository.GetById(userId);
			if (userTemporary == null)
			{
				return new ResponseDTO(HttpStatusCode.NotFound, MessageUser.UserNotFound);
			}

			User user = _mapper.Map<User>(userTemporary);
            user.UserId = userId;

			//active account
			user.Status = StatusAccount.ACTIVE;
			user.DateCreated = DateTimeHelper.GetDateTimeNow();
            await _unitOfWork.UserRepository.Add(user);

			if (await _unitOfWork.SaveChangesAsync())
			{
				//delete token
				await _unitOfWork.VerifyTokenRepository.Delete(userId);
				//delete user temporary
				await _unitOfWork.UserTemporaryRepository.Delete(userId);
				return new ResponseDTO(HttpStatusCode.OK, MessageUser.VerifySuccessfully);
			}
			return new ResponseDTO(HttpStatusCode.BadRequest, MessageUser.FailToVerify);
		}

		private async Task<ResponseDTO> VerifyToken(string token, Guid userId)
		{
			var verifyToken = await _unitOfWork.VerifyTokenRepository.GetById(userId);
			if (verifyToken == null)
			{
				return new ResponseDTO(HttpStatusCode.NotFound, MessageUser.NotFoundAccountToVerify);
			} else if (!verifyToken.Token.Equals(token))
			{
				return new ResponseDTO(HttpStatusCode.BadRequest, MessageUser.FailToVerifyToken);
			} else if (verifyToken.CreatedDate < DateTime.UtcNow)
			{
				return new ResponseDTO(HttpStatusCode.BadRequest, MessageUser.TokenExpired);
			}
			UserTemporary? userTemporary = await _unitOfWork.UserTemporaryRepository.GetById(userId);
			if (userTemporary == null)
			{
				return new ResponseDTO(HttpStatusCode.NotFound, MessageUser.UserNotFound);
			}

			User user = _mapper.Map<User>(userTemporary);

			//active account
			user.Status = StatusAccount.ACTIVE;
			user.DateCreated = DateTimeHelper.GetDateTimeNow();
            await _unitOfWork.UserRepository.Add(user);

			if (await _unitOfWork.SaveChangesAsync())
			{
                //delete token
                await _unitOfWork.VerifyTokenRepository.Delete(userId);
				//delete user temporary
				await _unitOfWork.UserTemporaryRepository.Delete(userId);
				return new ResponseDTO(HttpStatusCode.OK, MessageUser.VerifySuccessfully);
			}
			return new ResponseDTO(HttpStatusCode.BadRequest, MessageUser.FailToVerify);
		}
	}
}
