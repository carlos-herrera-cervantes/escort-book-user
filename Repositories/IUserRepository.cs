using System.Threading.Tasks;
using EscortBookUser.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace EscortBookUser.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(string userId);

    Task UpdateByIdAsync(string userId, User user, JsonPatchDocument<User> partialUser);
}
