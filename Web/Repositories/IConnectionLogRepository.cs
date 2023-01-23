using System.Threading.Tasks;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Web.Repositories;

public interface IConnectionLogRepository
{
    Task<ConnectionLog> GetByIdAsync(string userId);
}
