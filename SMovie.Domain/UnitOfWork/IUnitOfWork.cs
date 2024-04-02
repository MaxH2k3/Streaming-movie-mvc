using SMovie.Domain.Repository.Mongo;

namespace SMovie.Domain.Repository
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
