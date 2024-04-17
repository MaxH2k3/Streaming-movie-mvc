using SMovie.Application.IService;
using SMovie.Application.Message;
using SMovie.Application.MessageService;
using SMovie.Domain.Entity;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using System.Net;

namespace SMovie.Application.Service
{
    public class CastService : ICastService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CastService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> CreateCast(Guid movieId, IEnumerable<NewCast> newCasts)
        {
            LinkedList<Cast> casts = new LinkedList<Cast>();
            ResponseDTO responseDTO = await CheckExist(movieId, newCasts);

            if (responseDTO.Status != HttpStatusCode.OK)
            {
                return responseDTO;
            }

            foreach (var cast in newCasts)
            {
                casts.AddLast(new Cast()
                {
                    MovieId = movieId,
                    ActorId = cast.PersonId,
                    CharacterName = cast.CharacterName,
                });
            }

            await _unitOfWork.CastRepository.AddRange(casts);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return new ResponseDTO(HttpStatusCode.Created, MessageCommon.SavingSuccesfully);
            }

            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, MessageCommon.SavingFailed);
        }

        public Task<ResponseDTO> DeleteCast(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> UpdateCast(Guid movieId, IEnumerable<NewCast> newCasts)
        {
            LinkedList<Cast> casts = new LinkedList<Cast>();

            foreach (var cast in newCasts)
            {
                casts.AddLast(new Cast()
                {
                    MovieId = movieId,
                    ActorId = cast.PersonId,
                    CharacterName = cast.CharacterName,
                });
            }

            //get cast by movieId
            var existingCasts = await _unitOfWork.CastRepository.GetAll(c => c.MovieId == movieId);

            //delete cast not in newCasts
            var castsToDelete = existingCasts.Where(ec => !casts.Any(c => c.ActorId == ec.ActorId));

            await _unitOfWork.CastRepository.DeleteRange(castsToDelete);

            //update cast in newCasts
            await _unitOfWork.CastRepository.UpdateRange(casts);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return new ResponseDTO(HttpStatusCode.OK, MessageCommon.UpdateSuccesfully);
            }

            return new ResponseDTO(HttpStatusCode.ServiceUnavailable, MessageCommon.UpdateFailed);
        }

        private async Task<ResponseDTO> CheckExist(Guid movieId, IEnumerable<NewCast> newCasts)
        {
            //check exist movie
            var isExisted = await _unitOfWork.MovieRepository.IsExisted(movieId);
            if (!isExisted)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, MessageMovie.MovieNotFound, movieId);
            }
            //check exist person in cast of movie
            var casts = await _unitOfWork.CastRepository.GetAll(c => c.MovieId == movieId);
            foreach (var item in newCasts)
            {
                if (casts.Any(c => c.ActorId.Equals(item.PersonId)))
                {
                    return new ResponseDTO(HttpStatusCode.Conflict, MessagePerson.PersonNameExist, item);
                }
            }
            return new ResponseDTO(HttpStatusCode.OK, MessageCommon.ValidateSuccessfully);
        }
    }
}
