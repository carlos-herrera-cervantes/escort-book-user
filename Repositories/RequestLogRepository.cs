using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscortBookUser.Contexts;
using EscortBookUser.Models;
using Microsoft.EntityFrameworkCore;

namespace EscortBookUser.Repositories;

public class RequestLogRepository : IRequestLogRepository
{
    #region snippet_Properties

    private readonly EscortBookUserContext _context;

    #endregion

    #region snippet_Constructors

    public RequestLogRepository(EscortBookUserContext context) => _context = context;

    #endregion

    #region snippet_ActionMethods

    public async Task<IEnumerable<RequestLog>> GetAllAsync(string userId, int page, int pageSize)
        => await _context.RequestLogs.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

    #endregion
}
