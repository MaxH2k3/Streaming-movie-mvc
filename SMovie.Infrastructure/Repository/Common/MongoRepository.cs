using MongoDB.Driver;
using SMovie.Domain.Models;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;
using System.Linq.Expressions;

namespace SMovie.Infrastructure.Repository
{
    public class MongoRepository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        public MongoRepository()
        {
            _collection = new SMovieMongoContext().Database.GetCollection<T>(typeof(T).Name);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T?> GetById(dynamic id)
        {

            if(Guid.TryParse(id.ToString(), out Guid newId)) {
                return await _collection.Find(Builders<T>.Filter.Eq("MID", newId)).FirstOrDefaultAsync();
            }

            return null;   
        }

        public async Task Add(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async void Update(T entity)
        {
            var filter = Builders<T>.Filter.Eq("MID", GetEntityId(entity));
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task Delete(dynamic id)
        {
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("MID", id));
        }

        public async Task<bool> UpSert(T entity)
        {
            var filter = Builders<T>.Filter.Eq("MID", GetEntityId(entity));
            var result = await _collection.ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = true });
            return result.IsAcknowledged;
        }

        private static string? GetEntityId(T entity)
        {
            var property = typeof(T).GetProperty("MID");
            var idValue = property!.GetValue(entity);
            return idValue!.ToString();
        }

        public async Task<PagedList<T>> GetAll(int page, int eachPage)
        {
            var list = await _collection.Find(_ => true).ToListAsync();
            var total = list.Count;
            var items = list.Skip((page - 1) * eachPage).Take(eachPage);

            return new PagedList<T>(items, total, page, eachPage);
        }

        public async Task<PagedList<T>> GetAll(Expression<Func<T, bool>> predicate, int page, int eachPage)
        {
            var list = await _collection.Find(predicate).ToListAsync();
            var total = list.Count;
            var items = list.Skip((page - 1) * eachPage).Take(eachPage);

            return new PagedList<T>(items, total, page, eachPage);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).ToListAsync();
        }
    }
}
