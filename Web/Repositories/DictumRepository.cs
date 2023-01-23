using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EscortBookUser.Web.Contexts;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Web.Repositories;

public class DictumRepository : IDictumRepository
{
    #region snippet_Properties

    private readonly EscortBookUserContext _context;

    #endregion

    #region snippet_Constructors

    public DictumRepository(EscortBookUserContext context) => _context = context;

    #endregion

    #region snippet_ActionMethods

    public async Task<IEnumerable<Dictum>> GetAllAsync(string userId, int page, int pageSize)
        => await _context.Dictums.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

    public async Task CreateAsync(Dictum dictum)
    {
        await _context.Dictums.AddAsync(dictum);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(string id)
    {
        _context.Dictums.Remove(new Dictum { Id = id });
        await _context.SaveChangesAsync();
    }

    #endregion
}
