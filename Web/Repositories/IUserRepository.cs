using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Web.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(string userId);

    Task UpdateByIdAsync(string userId, User user, JsonPatchDocument<User> partialUser);
}
