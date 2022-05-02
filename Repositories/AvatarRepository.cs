using System.Threading.Tasks;
using EscortBookUser.Contexts;
using EscortBookUser.Models;
using Microsoft.EntityFrameworkCore;

namespace EscortBookUser.Repositories
{
    public class AvatarRepository : IAvatarRepository
    {
        #region snippet_Properties

        private readonly EscortBookUserContext _context;

        #endregion

        #region snippet_Constructors

        public AvatarRepository(EscortBookUserContext context) => _context = context;

        #endregion

        #region snippet_ActionMethods

        public async Task<Avatar> GetByIdAsync(string userId)
            => await _context.Avatars.AsNoTracking().FirstOrDefaultAsync(a => a.UserId == userId);

        public async Task CreateAsync(Avatar avatar)
        {
            await _context.Avatars.AddAsync(avatar);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateByIdAsync(Avatar avatar)
        {
            _context.Entry(avatar).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            _context.Avatars.Remove(new Avatar { Id = id });
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}