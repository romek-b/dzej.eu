using System.Threading.Tasks;
using DzejEu.Api.Models.Twitch.Authentication;

namespace DzejEu.Api.Clients.Twitch
{
    public interface ITwitchAuthClient
    {
        Task<TwitchToken> GetToken();
    }
}