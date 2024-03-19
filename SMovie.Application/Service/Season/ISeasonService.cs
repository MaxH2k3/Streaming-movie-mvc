

using SMovie.Domain.Entity;
using SMovie.Domain.Models;

namespace SMovie.Application.IService
{
    public interface ISeasonService
    {
        IEnumerable<Season> GetSeasons();
        Season GetSeason(Guid seasonId);
        IEnumerable<Season> GetSeasonsByMovie(Guid movieId);
        IEnumerable<Season> GetSeasonsByMovieAndNumber(Guid movieId, int seasonNumber);
        Task<ResponseDTO> CreateSeason(NewSeason newSeason);
        Task<ResponseDTO> DeleteSeason(Guid seasonId);
        Task<ResponseDTO> UpdateSeason(string name, Guid seasonId);
        Task<ResponseDTO> DeleteSeasonByMovie(Guid id);
    }
}
