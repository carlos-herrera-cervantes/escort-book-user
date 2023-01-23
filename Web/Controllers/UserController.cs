using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using EscortBookUser.Web.Models;
using EscortBookUser.Web.Repositories;

namespace EscortBookUser.Web.Controllers;

[Route("api/v1/users/profile")]
[ApiController]
public class UserController : ControllerBase
{
    #region snippet_Properties

    private readonly IUserRepository _userRepository;

    #endregion

    #region snippet_Constructors

    public UserController(IUserRepository userRepository)
        => _userRepository = userRepository;

    #endregion

    #region snippet_ActionMethods

    [HttpGet]
    public async Task<IActionResult> GetByIdAsync([FromHeader(Name = "user-id")] string userId)
    {
        User user = await _userRepository.GetByIdAsync(userId);

        if (user is null) return NotFound();

        return Ok(user);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateByIdAsync(
        [FromHeader(Name = "user-id")] string userId,
        [FromBody] JsonPatchDocument<User> partialUser
    )
    {
        User user = await _userRepository.GetByIdAsync(userId);

        if (user is null) return NotFound();

        await _userRepository.UpdateByIdAsync(userId, user, partialUser);

        return Ok(user);
    }

    #endregion
}
