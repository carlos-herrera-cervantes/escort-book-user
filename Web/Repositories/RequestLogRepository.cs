using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EscortBookUser.Web.Contexts;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Web.Repositories;

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
