using System.Collections.Generic;
using System.Threading.Tasks;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Web.Repositories;

public interface IRequestLogRepository
{
    Task<IEnumerable<RequestLog>> GetAllAsync(string userId, int page, int pageSize);
}
