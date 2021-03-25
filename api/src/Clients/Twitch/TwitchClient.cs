using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using DzejEu.Api.Models.Twitch;
using DzejEu.Api.Models.Twitch.Settings;
using DzejEu.Api.Providers.Twitch;
using Microsoft.Extensions.Options;

namespace DzejEu.Api.Clients.Twitch
{
    public class TwitchClient : ITwitchClient
    {
        private readonly ITwitchTokenProvider _tokenProvider;
        private readonly HttpClient _client;

        public TwitchClient(ITwitchTokenProvider tokenProvider, HttpClient client, IOptions<TwitchAppSettings> settings)
        {
            _tokenProvider = tokenProvider;
            _client = client;
            _client.BaseAddress = new Uri(settings.Value.Url);
            _client.DefaultRequestHeaders.Add("Client-Id", settings.Value.ClientId);
        }

        public async Task<StreamsDto> GetStreams(string userLogin)
        {
            var builder = new UriBuilder(_client.BaseAddress);

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["user_login"] = userLogin;

            builder.Path = "helix/streams";
            builder.Query = query.ToString();

            var url = builder.ToString();

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Authorization = await _tokenProvider.GetAuthorizationHeader();

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<StreamsDto>(responseStream, JsonOptions);
        }

        private JsonSerializerOptions JsonOptions => new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true,
        };
    }
}