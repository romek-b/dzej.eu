using System.Threading.Tasks;
using DzejEu.Api.Models.Twitch;

namespace DzejEu.Api.Clients.Twitch
{
    public interface ITwitchClient
    {
        Task<StreamsDto> GetStreams(string userLogin);
    }
}