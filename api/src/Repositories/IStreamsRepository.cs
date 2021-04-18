using System.Threading.Tasks;
using DzejEu.Api.Models.Database;
using MongoDB.Bson;

namespace DzejEu.Api.Repositories
{
    public interface IStreamsRepository
    {
        Task Add(Stream stream);
        Task<Stream> GetNewest(string username);
        Task<Stream> GetNewestByGameNamePattern(string username, string pattern);
        Task SetIsOngoing(ObjectId id, bool isOngoing);
    }
}