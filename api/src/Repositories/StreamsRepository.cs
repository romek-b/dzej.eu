using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DzejEu.Api.Clients.Database;
using DzejEu.Api.Models.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DzejEu.Api.Repositories
{
    public class StreamsRepository : IStreamsRepository
    {
        private readonly DatabaseClient _db;

        public StreamsRepository(DatabaseClient db)
        {
            _db = db;
        }

        public async Task Add(Stream stream)
        {
            await _db.Streams.InsertOneAsync(stream);
        }

        public async Task MarkAsFinished(ObjectId id)
        {
            var filter = Builders<Stream>.Filter.Eq(x => x.Id, id);
            var update = Builders<Stream>.Update.Set(x => x.IsOngoing, false);

            await _db.Streams.FindOneAndUpdateAsync(filter, update);
        }

        public async Task<Stream> GetNewest(string username)
        {
            return await _db.Streams.Find(x => x.UserName == username).SortByDescending(x => x.StartedAt).FirstOrDefaultAsync();
        }

        public async Task<Stream> GetNewestByGameNamePattern(string username, string pattern)
        {
            var regex = new Regex($"^.*{pattern}.*$", RegexOptions.IgnoreCase);
            var userFilter = Builders<Stream>.Filter.Eq(x => x.UserName, username);
            var gameFilter = Builders<Stream>.Filter.Regex(x => x.GameName, new BsonRegularExpression(regex));
            var filter = Builders<Stream>.Filter.And(userFilter, gameFilter);
            return await _db.Streams.Find(filter).SortByDescending(x => x.StartedAt).FirstOrDefaultAsync();
        }
    }
}