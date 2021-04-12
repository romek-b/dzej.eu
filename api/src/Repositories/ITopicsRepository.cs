using System.Collections.Generic;
using System.Threading.Tasks;
using DzejEu.Api.Models.Database;

namespace DzejEu.Api.Repositories
{
    public interface ITopicsRepository
    {
        Task<IEnumerable<Topic>> GetAll();
    }
}