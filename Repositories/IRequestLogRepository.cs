using System.Collections.Generic;
using System.Threading.Tasks;
using EscortBookUser.Models;

namespace EscortBookUser.Repositories;

public interface IRequestLogRepository
{
    Task<IEnumerable<RequestLog>> GetAllAsync(string userId, int page, int pageSize);
}
