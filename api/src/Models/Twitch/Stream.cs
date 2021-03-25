using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DzejEu.Api.Models.Twitch
{
    public class Stream
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        [JsonPropertyName("user_login")]
        public string UserLogin { get; set; }

        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        [JsonPropertyName("game_id")]
        public long GameId { get; set; }

        [JsonPropertyName("game_name")]
        public string GameName { get; set; }

        public string Type { get; set; }

        public string Title { get; set; }

        [JsonPropertyName("viewer_count")]
        public long ViewerCount { get; set; }

        [JsonPropertyName("started_at")]
        public DateTime StartedAt { get; set; }

        public string Language { get; set; }

        [JsonPropertyName("thumbnail_url")]
        public string ThumbnailUrl { get; set; }

        [JsonPropertyName("tag_ids")]
        public IEnumerable<Guid> TagIds { get; set; }
    }
}