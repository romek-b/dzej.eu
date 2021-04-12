using DzejEu.Api.Models.Database;
using DzejEu.Api.Models.Database.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DzejEu.Api.Clients.Database
{
    public class DatabaseClient
    {
        private readonly IMongoDatabase _database;

        public DatabaseClient(IOptions<DatabaseSettings> dbSettings)
        {
            var client = new MongoClient(dbSettings.Value.ConnectionString);
            _database = client.GetDatabase(dbSettings.Value.DatabaseName);
        }

        public IMongoCollection<Stream> Streams => _database.GetCollection<Stream>("streams");

        public IMongoCollection<Topic> Topics => _database.GetCollection<Topic>("topics");
    }
}