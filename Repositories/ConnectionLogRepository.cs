using System.Threading.Tasks;
using EscortBookUser.Contexts;
using EscortBookUser.Models;
using Microsoft.EntityFrameworkCore;

namespace EscortBookUser.Repositories
{
    public class ConnectionLogRepository : IConnectionLogRepository
    {
        #region snippet_Properties

        private readonly EscortBookUserContext _context;

        #endregion

        #region snippet_Constructors

        public ConnectionLogRepository(EscortBookUserContext context) => _context = context;

        #endregion

        #region snippet_ActionMethods

        public async Task<ConnectionLog> GetByIdAsync(string userId)
            => await _context.ConnectionLogs.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == userId);

        #endregion
    }
}