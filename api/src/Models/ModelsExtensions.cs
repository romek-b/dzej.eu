using DzejEu.Api.Models.Database;

namespace DzejEu.Api.Models
{
    public static class ModelsExtensions
    {
        public static StreamDto ToDto(this Stream stream)
        {
            return new StreamDto
            {
                UserName = stream.UserName,
                GameName = stream.GameName,
                StartedAt = stream.StartedAt,
                IsOngoing = stream.IsOngoing
            };
        }
    }
}