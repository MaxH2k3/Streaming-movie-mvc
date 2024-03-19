using SMovie.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMovie.Domain.Repository
{
    public interface IMovieCategoryRepository : IExtendRepository<MovieCategory>
    {
        void DeleteMovieCategory(IEnumerable<MovieCategory> categories);
        Task<IEnumerable<MovieCategory>> GetMovieCategoryByMovieId(Guid movieId);
        Task Add(Guid movieId, IEnumerable<int> movieCategories);
    }
}
