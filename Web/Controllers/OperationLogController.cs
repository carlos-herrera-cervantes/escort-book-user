using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EscortBookUser.Web.Repositories;
using EscortBookUser.Web.Types;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Web.Controllers;

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
    public async Task<IActionResult> GetAllAsync(
        [FromRoute] string userid,
        [FromQuery] Pager pager
    )
    {
        var (page, pageSize) = pager;
        IEnumerable<RequestLog> operationLogs = await _requestLogRepository.GetAllAsync(userid, page, pageSize);
        return Ok(operationLogs);
    }

    #endregion
}
