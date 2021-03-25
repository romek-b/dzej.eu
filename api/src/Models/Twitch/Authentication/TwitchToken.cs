using System.Text.Json.Serialization;

namespace DzejEu.Api.Models.Twitch.Authentication
{
    public class TwitchToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
    }
}