using System.Threading.Tasks;
using EscortBookUser.Repositories;
using EscortBookUser.Types;
using Microsoft.AspNetCore.Mvc;

namespace EscortBookUser.Controllers
{
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
            => Ok(await _requestLogRepository.GetAllAsync(userid, pager.Page, pager.PageSize));

        #endregion
    }
}
