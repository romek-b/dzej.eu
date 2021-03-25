using System.Collections.Generic;

namespace DzejEu.Api.Models.Twitch
{
    public class StreamsDto
    {
        public IEnumerable<Stream> Data { get; set; }
        
        public Pagination Pagination { get; set; }
    }
}