using Microsoft.IdentityModel.Tokens;
using SMovie.Application.IService;
using SMovie.Domain.Constant;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using SMovie.Domain.UnitOfWork;
using SMovie.Infrastructure.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SMovie.Application.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JWTSetting _jwtsetting;

        public AuthenticationService(IUnitOfWork unitOfWork, JWTSetting jWTSetting)
        {
            _unitOfWork = unitOfWork;
            _jwtsetting = jWTSetting;
        }

        public AuthenticationService()
        {
            _unitOfWork = new UnitOfWork();
            _jwtsetting = new JWTSetting();
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        public async Task<string> GenerateToken(UserDTO userDTO)
        {
            var user = await _unitOfWork.UserRepository.GetUser(userDTO.UserName!);
            if (user == null)
            {
                return "";
            }
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(_jwtsetting.SecurityKey!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new(ClaimTypesUser.UserId, user.UserId.ToString()),
                        new(ClaimTypesUser.Status, user.Status!),
                        new(ClaimTypesUser.Role, user.Role!)
                    }
                ),
                Expires = DateTime.Now.AddMinutes((double)_jwtsetting.TokenExpiry!),
                Issuer = _jwtsetting.Issuer,
                Audience = _jwtsetting.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            string finaltoken = tokenhandler.WriteToken(token);

            return finaltoken;
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac
                .ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
