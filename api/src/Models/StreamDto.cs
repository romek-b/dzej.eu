using System;

namespace DzejEu.Api.Models
{
    public class StreamDto
    {
        public string UserName { get; set; }

        public string GameName { get; set; }

        public DateTime StartedAt { get; set; }

        public bool IsOngoing { get; set; }
    }
}