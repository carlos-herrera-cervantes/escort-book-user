using System.Collections.Generic;
using System.Threading.Tasks;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Web.Repositories;

public interface IStatusCategoryRepository
{
    Task<IEnumerable<StatusCategory>> GetAllAsync(int page, int pageSize);
}
