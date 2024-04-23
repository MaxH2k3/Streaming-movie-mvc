using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;

namespace SMovie.Infrastructure.Repository
{
    public class PreviousTopMovieRepository : MongoRepository<AnalystMovie>, IAnalystMovieRepository
    {
        private readonly SMovieMongoContext _context;

        public PreviousTopMovieRepository(SMovieMongoContext context) : base(context.PreviousTopMovies)
        {
            _context = context;
        }

        public async Task ConvertToPrevious()
        {
            var pipeline = new List<BsonDocument>
            {
                new("$sort", new BsonDocument("Viewer", -1)),
                new("$limit", 15),
                new("$out", "CurrentTopMovie")
            };
            var list = await _context.CurrentTopMovies.AggregateAsync<BsonDocument>(pipeline).Result.ToListAsync();
            if (list.Count > 0)
            {
                await _context.PreviousTopMovies.DeleteManyAsync(new BsonDocument());
            }
        }

        public async Task UpSert(Guid movieId)
        {
            var filter = Builders<AnalystMovie>.Filter.Eq("MovieId", movieId);
            var update = Builders<AnalystMovie>.Update.Inc("Viewer", 1);
            var options = new UpdateOptions { IsUpsert = true };
            await _context.PreviousTopMovies.UpdateOneAsync(filter, update, options);
        }
    }
}
