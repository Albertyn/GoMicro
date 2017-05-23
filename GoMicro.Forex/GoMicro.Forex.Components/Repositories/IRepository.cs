using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoMicro.Forex.Components.Repositories
{
    public interface IRepository<T>
    {
        Task DropDatabase();
        Task DropCollection();
        Task DropCollection(string collectionName);


        Task<T> Get(BsonDocument filter);
        Task<T> Get(BsonDocument filter, string collectionName);
        Task<T> Get(FilterDefinition<T> filter);
        Task<T> Get(FilterDefinition<T> filter, string collectionName);


        Task<T> Add(T model);
        Task<T> Add(T model, string collectionName);
        Task Add(List<T> models);
        Task Add(List<T> models, string collectionName);


        Task<T> Upsert(FilterDefinition<T> filter, T document);
        Task<T> Upsert(FilterDefinition<T> filter, T document, string collectionName);


        Task Detele(FilterDefinition<T> filter);
        Task Delete(FilterDefinition<T> filter, string collectionName);

        Task<List<T>> All();
        Task<List<T>> All(string collectionName);
        Task<List<T>> All(BsonDocument filter);
        Task<List<T>> All(BsonDocument filter, string collectionName);
        Task<List<T>> All(FilterDefinition<T> filter);
        Task<List<T>> All(FilterDefinition<T> filter, string collectionName);

        Task<List<T>> Page(int Page, int PageSize);

    }
}
