using System.Collections.Generic;
using System.Threading.Tasks;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Web.Repositories;

public interface IDictumRepository
{
    Task<IEnumerable<Dictum>> GetAllAsync(string userId, int page, int pageSize);

    Task CreateAsync(Dictum dictum);

    Task DeleteByIdAsync(string id);
}
