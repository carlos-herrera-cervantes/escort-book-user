using System.Threading.Tasks;
using EscortBookUser.Models;

namespace EscortBookUser.Repositories;

public interface IConnectionLogRepository
{
    Task<ConnectionLog> GetByIdAsync(string userId);
}
