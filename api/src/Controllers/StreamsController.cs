using System.Threading.Tasks;
using DzejEu.Api.Models;
using DzejEu.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DzejEu.Api.Services
{
    [ApiController]
    [Route("api/[controller]")]
    public class StreamsController : ControllerBase
    {
        private readonly IStreamsRepository _repository;

        public StreamsController(IStreamsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{streamerName}")]
        public async Task<StreamDto> GetNewest(string streamerName)
        {
            var result = await _repository.GetNewest(streamerName);
            return result?.ToDto();
        }

        [HttpGet("{streamerName}/{gamePattern}")]
        public async Task<StreamDto> GetNewestByPattern(string streamerName, string gamePattern)
        {
            var result = await _repository.GetNewestByGameNamePattern(streamerName, gamePattern);
            return result?.ToDto();
        }
    }
}