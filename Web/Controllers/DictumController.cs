using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EscortBookUser.Web.Models;
using EscortBookUser.Web.Repositories;

namespace EscortBookUser.Web.Controllers;

[Route("api/v1/users/{id}/dictums")]
[ApiController]
public class DictumController : ControllerBase
{
    #region snippet_Properties

    private readonly IDictumRepository _dictumRepository;

    #endregion

    #region snippet_Consturctors

    public DictumController(IDictumRepository dictumRepository)
        => _dictumRepository = dictumRepository;

    #endregion

    #region snippet_ActionMethods

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromRoute] string userId)
        => Ok(await _dictumRepository.GetAllAsync(userId, 0, 10));

    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromHeader(Name = "user-id")] string userId,
        [FromRoute] string id,
        Dictum dictum
    )
    {
        dictum.UserId = id;
        dictum.FromUser = userId;
        await _dictumRepository.CreateAsync(dictum);
        return Created("", dictum);
    }

    [HttpDelete("{dictumId}")]
    public async Task<IActionResult> DeleteByIdAsync(
        [FromRoute] string userId,
        [FromRoute] string dictumId
    )
    {
        await _dictumRepository.DeleteByIdAsync(dictumId);
        return NoContent();
    }

    #endregion
}
