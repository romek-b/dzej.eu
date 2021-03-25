using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using DzejEu.Api.Models.Twitch.Authentication;
using DzejEu.Api.Models.Twitch.Settings;
using Microsoft.Extensions.Options;

namespace DzejEu.Api.Clients.Twitch
{
    public class TwitchAuthClient : ITwitchAuthClient
    {
        private readonly HttpClient _client;
        private readonly IOptions<TwitchAppSettings> _settings;

        public TwitchAuthClient(HttpClient client,
            IOptions<TwitchAppSettings> settings)
        {
            _client = client;
            _settings = settings;
            _client.BaseAddress = new Uri(_settings.Value.AuthUrl);
        }

        public async Task<TwitchToken> GetToken()
        {
            var builder = new UriBuilder(_client.BaseAddress);

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["client_id"] = _settings.Value.ClientId;
            query["client_secret"] = _settings.Value.ClientSecret;
            query["grant_type"] = _settings.Value.GrantType;

            builder.Path = "oauth2/token";
            builder.Query = query.ToString();

            var url = builder.ToString();

            var response = await _client.PostAsync(url, null);
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TwitchToken>(responseStream);
        }
    }
}