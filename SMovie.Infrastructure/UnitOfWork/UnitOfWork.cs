using SMovie.Domain.Repository;
using SMovie.Domain.Repository.Mongo;
using SMovie.Domain.UnitOfWork;
using SMovie.Infrastructure.DBContext;
using SMovie.Infrastructure.Repository;

namespace SMovie.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SMovieSQLContext _contextSQL;
        private readonly SMovieMongoContext _contextMongo;
        private readonly ICastRepository _castRepository = null!;
        private readonly ICategoryRepository _categoryRepository = null!;
        private readonly IEpisodeRepository _episodeRepository = null!;
        private readonly IFeatureRepository _featureRepository = null!;
        private readonly IMovieCategoryRepository _movieCategoryRepository = null!;
        private readonly IMovieRepository _movieRepository = null!;
        private readonly INationRepository _nationRepository = null!;
        private readonly IPersonRepository _personRepository = null!;
        private readonly ISeasonRepository _seasonRepository = null!;
        private readonly IUserRepository _userRepository = null!;
        private readonly IAnalystMovieRepository _currentTopMovieRepository = null!;
        private readonly IAnalystMovieRepository _previousTopMovieRepository = null!;
        private readonly IBlackIPRepository _blackIPRepository = null!;
        private readonly IUserTemporaryRepository _userTemporaryRepository = null!;
        private readonly IVerifyTokenRepository _verifyTokenRepository = null!;

        public UnitOfWork(SMovieSQLContext contextSQL, SMovieMongoContext contextMongo)
        {
            _contextSQL = contextSQL;
            _contextMongo = contextMongo;
        }

        public UnitOfWork()
        {
            _contextSQL = new SMovieSQLContext();
            _contextMongo = new SMovieMongoContext();
        }

        public ICastRepository CastRepository => _castRepository ?? new CastRepository(_contextSQL);

        public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(_contextSQL);

        public IEpisodeRepository EpisodeRepository => _episodeRepository ?? new EpisodeRepository(_contextSQL);

        public IFeatureRepository FeatureRepository => _featureRepository ?? new FeatureRepository(_contextSQL);

        public IMovieCategoryRepository MovieCategoryRepository => _movieCategoryRepository ?? new MovieCategoryRepository(_contextSQL);

        public IMovieRepository MovieRepository => _movieRepository ?? new MovieRepository(_contextSQL);

        public INationRepository NationRepository => _nationRepository ?? new NationRepository(_contextSQL);

        public IPersonRepository PersonRepository => _personRepository ?? new PersonRepository(_contextSQL);

        public ISeasonRepository SeasonRepository => _seasonRepository ?? new SeasonRepository(_contextSQL);

        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_contextSQL);

        public IAnalystMovieRepository CurrentTopMovieRepository => _currentTopMovieRepository ?? new CurrentTopMovieRepository(_contextMongo);

        public IAnalystMovieRepository PreviousTopMovieRepository => _previousTopMovieRepository ?? new PreviousTopMovieRepository(_contextMongo);

        public IBlackIPRepository BlackIPRepository => _blackIPRepository ?? new BlackIPRepository(_contextMongo);

        public IUserTemporaryRepository UserTemporaryRepository => _userTemporaryRepository ?? new UserTemporaryRepository(_contextMongo);

        public IVerifyTokenRepository VerifyTokenRepository => _verifyTokenRepository ?? new VerifyTokenRepository(_contextMongo);

        //release resources when we are done with them (not using the context anymore)
        public void Dispose()
        {
            _contextSQL.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _contextSQL.SaveChangesAsync() > 0;
        }
    }
}
