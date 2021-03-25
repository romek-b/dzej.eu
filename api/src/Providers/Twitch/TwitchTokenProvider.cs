using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DzejEu.Api.Clients.Twitch;
using DzejEu.Api.Models.Twitch.Authentication;
using Microsoft.Extensions.Caching.Memory;

namespace DzejEu.Api.Providers.Twitch
{
    public class TwitchTokenProvider : ITwitchTokenProvider
    {
        private readonly IMemoryCache _cache;
        private readonly ITwitchAuthClient _client;

        public TwitchTokenProvider(IMemoryCache cache, ITwitchAuthClient client)
        {
            _cache = cache;
            _client = client;
        }

        public async Task<AuthenticationHeaderValue> GetAuthorizationHeader()
        {
            var token = await _cache.GetOrCreateAsync<TwitchToken>("twitch_token", entry =>
            {
                var newToken = _client.GetToken().GetAwaiter().GetResult();
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(newToken.ExpiresIn));
                entry.SetValue(newToken);
                return Task.FromResult(newToken);
            });
            return new AuthenticationHeaderValue("Bearer", token.AccessToken);
        }
    }
}