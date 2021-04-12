using System.Collections.Generic;
using System.Threading.Tasks;
using DzejEu.Api.Clients.Database;
using DzejEu.Api.Models.Database;
using MongoDB.Driver;

namespace DzejEu.Api.Repositories
{
    public class TopicsRepository : ITopicsRepository
    {
        private readonly DatabaseClient _db;

        public TopicsRepository(DatabaseClient db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Topic>> GetAll()
        {
            return await _db.Topics.Find(x => true).ToListAsync();
        }
    }
}