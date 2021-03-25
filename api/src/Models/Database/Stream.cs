using System;
using MongoDB.Bson;

namespace DzejEu.Api.Models.Database
{
    public class Stream
    {
        public ObjectId Id { get; set; }

        public long TwitchId { get; set; }

        public string UserName { get; set; }

        public string GameName { get; set; }

        public DateTime StartedAt { get; set; }

        public bool IsOngoing { get; set; }
    }
}