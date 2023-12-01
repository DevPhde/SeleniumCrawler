using IPrazos.Entity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crawler.Repository
{
    public static class ProxyDataRepository
    {
        private static readonly IMongoCollection<ProxyData> _collection;

        static ProxyDataRepository()
        {
            string connectionString = "mongodb://localhost:27017";
            string databaseName = "iPrazos";
            string collectionName = "ProxyData";

            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            var client = new MongoClient(settings);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<ProxyData>(collectionName);
        }

        public static async Task<List<ProxyData>> GetAllProxyConnection()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public static async Task SaveProxyData(ProxyData proxyData)
        {
            await _collection.InsertOneAsync(proxyData);
        }
    }
}
