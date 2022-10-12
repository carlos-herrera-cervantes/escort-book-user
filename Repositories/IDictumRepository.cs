using System.Collections.Generic;
using System.Threading.Tasks;
using EscortBookUser.Models;

namespace EscortBookUser.Repositories;

public interface IDictumRepository
{
    Task<IEnumerable<Dictum>> GetAllAsync(string userId, int page, int pageSize);

    Task CreateAsync(Dictum dictum);

    Task DeleteByIdAsync(string id);
}
