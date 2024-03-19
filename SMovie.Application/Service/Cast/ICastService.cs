
using SMovie.Domain.Models;

namespace SMovie.Application.IService
{
    public interface ICastService
    {
        Task<ResponseDTO> CreateCast(Guid movieId, IEnumerable<NewCast> newCasts);
        Task<ResponseDTO> UpdateCast(Guid movieId, IEnumerable<NewCast> newCasts);
        Task<ResponseDTO> DeleteCast(int id);
    }
}
