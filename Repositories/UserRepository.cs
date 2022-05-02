using System.Threading.Tasks;
using EscortBookUser.Contexts;
using EscortBookUser.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace EscortBookUser.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region snippet_Properties

        private readonly EscortBookUserContext _context;

        #endregion

        #region snippet_Constructors

        public UserRepository(EscortBookUserContext context) => _context = context;

        #endregion

        #region snippet_ActionMethods

        public async Task<User> GetByIdAsync(string userId)
            => await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == userId);

        public async Task UpdateByIdAsync(string userId, User user, JsonPatchDocument<User> partialUser)
        {
            partialUser.ApplyTo(user);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}