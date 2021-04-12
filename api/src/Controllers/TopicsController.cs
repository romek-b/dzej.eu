using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DzejEu.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DzejEu.Api.Services
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicsRepository _repository;

        public TopicsController(ITopicsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetAll()
        {
            var result = await _repository.GetAll();
            return result.Select(x => x.Value);
        }
    }
}