using System.Collections.Generic;
using System.Threading.Tasks;
using EscortBookUser.Models;

namespace EscortBookUser.Repositories
{
    public interface IStatusCategoryRepository
    {
         Task<IEnumerable<StatusCategory>> GetAllAsync(int page, int pageSize);
    }
}
