using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoMicro.Forex.Components.Repositories
{
    public abstract class Repository<T> : IRepository<T>
    {
        private readonly MongoUrl _Url;
        protected readonly MongoClient _Client;
        protected readonly IMongoDatabase _Database;

        protected Repository(IMongoConfiguration configuration)
        {
            _Url = new MongoUrl(configuration.ConnectionString);
            _Client = new MongoClient(configuration.ConnectionString);
            _Database = _Client.GetDatabase(_Url.DatabaseName);
        }

        protected abstract string CollectionName { get; set; }

        public virtual async Task DropDatabase()
        {
            await _Client.DropDatabaseAsync(_Url.DatabaseName);
        }
        public virtual async Task DropCollection()
        {
            await _Database.DropCollectionAsync(CollectionName);
        }
        public virtual async Task DropCollection(string collectionName)
        {
            await _Database.DropCollectionAsync(collectionName);
        }


        #region GET
        public virtual async Task<T> Get(BsonDocument filter)
        {
            var collection = _Database.GetCollection<T>(CollectionName);
            var document = await collection.FindAsync<T>(filter);
            return document.FirstOrDefault();
        }
        public virtual async Task<T> Get(BsonDocument filter, string collectionName)
        {
            var collection = _Database.GetCollection<T>(collectionName);
            var document = await collection.FindAsync<T>(filter);
            return document.FirstOrDefault();
        }
        public virtual async Task<T> Get(FilterDefinition<T> filter)
        {
            var collection = _Database.GetCollection<T>(CollectionName);
            var document = await collection.FindAsync<T>(filter);
            return document.FirstOrDefault();
        }
        public virtual async Task<T> Get(FilterDefinition<T> filter, string collectionName)
        {
            var collection = _Database.GetCollection<T>(collectionName);
            var document = await collection.FindAsync<T>(filter);
            return document.FirstOrDefault();
        }
        #endregion


        public virtual async Task<T> Upsert(FilterDefinition<T> filter, T document)
        {
            var collection = _Database.GetCollection<T>(CollectionName);
            await collection.ReplaceOneAsync(filter, document, new UpdateOptions
            {
                IsUpsert = true,
                BypassDocumentValidation = true
            });
            return document;
        }
        public virtual async Task<T> Upsert(FilterDefinition<T> filter, T document, string collectionName)
        {
            var collection = _Database.GetCollection<T>(collectionName);
            await collection.ReplaceOneAsync(filter, document, new UpdateOptions { IsUpsert = true });
            return document;
        }

        public virtual async Task Detele(FilterDefinition<T> filter)
        {
            var collection = _Database.GetCollection<T>(CollectionName);
            await collection.DeleteOneAsync(filter);
        }
        public virtual async Task Delete(FilterDefinition<T> filter, string collectionName)
        {
            var collection = _Database.GetCollection<T>(collectionName);
            await collection.DeleteOneAsync(filter);
        }


        #region ADD
        public virtual async Task<T> Add(T document)
        {
            var collection = _Database.GetCollection<T>(CollectionName);
            await collection.InsertOneAsync(document);
            return document;
        }
        public virtual async Task<T> Add(T document, string collectionName)
        {
            var collection = _Database.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(document);
            return document;
        }
        public virtual async Task Add(List<T> list)
        {
            var collection = _Database.GetCollection<T>(CollectionName);
            var writes = list.Select(doc => new InsertOneModel<T>(doc)).Cast<WriteModel<T>>().ToList();
            await collection.BulkWriteAsync(writes, new BulkWriteOptions { IsOrdered = false });
        }
        public virtual async Task Add(List<T> list, string collectionName)
        {
            var collection = _Database.GetCollection<T>(collectionName);
            var writes = list.Select(doc => new InsertOneModel<T>(doc)).Cast<WriteModel<T>>().ToList();
            await collection.BulkWriteAsync(writes, new BulkWriteOptions { IsOrdered = false });
        }
        #endregion


        #region ALL
        public virtual async Task<List<T>> All()
        {
            var collection = _Database.GetCollection<T>(CollectionName);
            var documents = await collection.FindAsync<T>(new BsonDocument());
            return await documents.ToListAsync();
        }
        public virtual async Task<List<T>> All(string collectionName)
        {
            var collection = _Database.GetCollection<T>(collectionName);
            var documents = await collection.FindAsync<T>(new BsonDocument());
            return await documents.ToListAsync();
        }
        public virtual async Task<List<T>> All(BsonDocument filter)
        {
            var collection = _Database.GetCollection<T>(CollectionName);
            var documents = await collection.FindAsync<T>(filter);
            return await documents.ToListAsync();
        }
        public virtual async Task<List<T>> All(BsonDocument filter, string collectionName)
        {
            var collection = _Database.GetCollection<T>(collectionName);
            var documents = await collection.FindAsync<T>(filter);
            return await documents.ToListAsync();
        }
        public virtual async Task<List<T>> All(FilterDefinition<T> filter)
        {
            var collection = _Database.GetCollection<T>(CollectionName);
            var documents = await collection.FindAsync<T>(filter);
            return await documents.ToListAsync();
        }
        public virtual async Task<List<T>> All(FilterDefinition<T> filter, string collectionName)
        {
            var collection = _Database.GetCollection<T>(collectionName);
            var document = await collection.FindAsync<T>(filter);
            return await document.ToListAsync();
        }
        #endregion

        public virtual async Task<List<T>> Page(int Page, int PageSize)
        {
            var collection = _Database.GetCollection<T>(CollectionName);
            var cursor = await collection.FindAsync(new BsonDocument(), new FindOptions<T, T>
            {
                Limit = PageSize,
                Skip = (Page - 1) * PageSize
            });
            return await cursor.ToListAsync();
        }
        public virtual async Task SetExpiration(int seconds)
        {
            var collection = _Database.GetCollection<T>(CollectionName);
            var index = Builders<T>.IndexKeys.Ascending("CreatedOn");
            await collection.Indexes.CreateOneAsync(index, new CreateIndexOptions
            {
                ExpireAfter = new TimeSpan(0, 0, seconds)
            });
        }
    }
}
