using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EscortBookUser.Web.Repositories;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Web.Controllers;

[Route("api/v1/users/{id}/last-connection")]
[ApiController]
public class ConnectionLogController : ControllerBase
{
    #region snippet_Properties

    private readonly IConnectionLogRepository _connectionLogRepository;

    #endregion

    #region snippet_Constructors

    public ConnectionLogController(IConnectionLogRepository connectionLogRepository)
        => _connectionLogRepository = connectionLogRepository;

    #endregion

    #region snippet_ActionMethods

    [HttpGet]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
    {
        ConnectionLog lastConnection = await _connectionLogRepository.GetByIdAsync(id);

        if (lastConnection is null) return NotFound();

        return Ok(lastConnection);
    }

    #endregion
}
