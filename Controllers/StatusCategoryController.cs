using System.Threading.Tasks;
using EscortBookUser.Repositories;
using EscortBookUser.Types;
using Microsoft.AspNetCore.Mvc;

namespace EscortBookUser.Controllers
{
    [Route("api/v1/users/status-category")]
    [ApiController]
    public class StatusCategoryController : ControllerBase
    {
        #region snippet_Properties

        private readonly IStatusCategoryRepository _statusCategoryRepository;

        #endregion

        #region snippet_Constructors

        public StatusCategoryController(IStatusCategoryRepository statusCategoryRepository)
            => _statusCategoryRepository = statusCategoryRepository;

        #endregion

        #region snippet_ActionMethods

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] Pager pager)
            => Ok(await _statusCategoryRepository.GetAllAsync(pager.Page, pager.PageSize));

        #endregion
    }
}
