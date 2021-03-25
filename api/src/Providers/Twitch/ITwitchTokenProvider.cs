using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DzejEu.Api.Providers.Twitch
{
    public interface ITwitchTokenProvider
    {
        Task<AuthenticationHeaderValue> GetAuthorizationHeader();
    }
}