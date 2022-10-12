using System.Threading.Tasks;
using EscortBookUser.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EscortBookUser.Controllers;

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
        var lastConnection = await _connectionLogRepository.GetByIdAsync(id);

        if (lastConnection is null) return NotFound();

        return Ok(lastConnection);
    }

    #endregion
}
