using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EscortBookUser.Web.Contexts;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Web.Repositories;

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
        => await _context.ConnectionLogs
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.UserId == userId);

    #endregion
}
