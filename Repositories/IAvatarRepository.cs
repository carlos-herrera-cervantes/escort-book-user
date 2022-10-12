using System.Threading.Tasks;
using EscortBookUser.Models;

namespace EscortBookUser.Repositories;

public interface IAvatarRepository
{
    Task<Avatar> GetByIdAsync(string userId);

    Task CreateAsync(Avatar avatar);

    Task UpdateByIdAsync(Avatar avatar);

    Task DeleteByIdAsync(string id);
}
