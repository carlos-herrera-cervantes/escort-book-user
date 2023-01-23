using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EscortBookUser.Web.Repositories;
using EscortBookUser.Web.Types;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Web.Controllers;

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
    {
        var (page, pageSize) = pager;
        IEnumerable<StatusCategory> status = await _statusCategoryRepository.GetAllAsync(page, pageSize);
        return Ok(status);
    }

    #endregion
}
