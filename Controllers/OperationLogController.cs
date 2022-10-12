using System.Threading.Tasks;
using EscortBookUser.Repositories;
using EscortBookUser.Types;
using Microsoft.AspNetCore.Mvc;

namespace EscortBookUser.Controllers;

[Route("api/v1/users/{id}/operation-log")]
[ApiController]
public class OperationLogController : ControllerBase
{
    #region snippet_Properties

    private readonly IRequestLogRepository _requestLogRepository;

    #endregion

    #region snippet_Constructor

    public OperationLogController(IRequestLogRepository requestLogRepository)
        => _requestLogRepository = requestLogRepository;

    #endregion

    #region snippet_ActionMethods

    [HttpGet]
    public async Task<IActionResult> GetAllAsync
    (
        [FromRoute] string userid,
        [FromQuery] Pager pager
    )
    {
        var (page, pageSize) = pager;
        var operationLogs = await _requestLogRepository.GetAllAsync(userid, page, pageSize);
        return Ok(operationLogs);
    }

    #endregion
}
