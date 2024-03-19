using SMovie.Domain.Repository;
using SMovie.Domain.Repository.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMovie.Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICastRepository CastRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IEpisodeRepository EpisodeRepository { get; }
        IFeatureRepository FeatureRepository { get; }
        IMovieCategoryRepository MovieCategoryRepository { get; }
        IMovieRepository MovieRepository { get; }
        INationRepository NationRepository { get; }
        IPersonRepository PersonRepository { get; }
        ISeasonRepository SeasonRepository { get; }
        IUserRepository UserRepository { get; }
        IAnalystMovieRepository CurrentTopMovieRepository { get; }
        IAnalystMovieRepository PreviousTopMovieRepository { get; }
        IBlackIPRepository BlackIPRepository { get; }
        IUserTemporaryRepository UserTemporaryRepository { get; }
        IVerifyTokenRepository VerifyTokenRepository { get; }

        Task<bool> SaveChangesAsync();
    }
}
