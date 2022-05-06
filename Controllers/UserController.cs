using System.Threading.Tasks;
using EscortBookUser.Models;
using EscortBookUser.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace EscortBookUser.Controllers
{
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
            => Ok(await _userRepository.GetByIdAsync(userId));

        [HttpPatch]
        public async Task<IActionResult> UpdateByIdAsync
        (
            [FromHeader(Name = "user-id")] string userId,
            [FromBody] JsonPatchDocument<User> partialUser
        )
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null) return NotFound();

            await _userRepository.UpdateByIdAsync(userId, user, partialUser);

            return Ok(user);
        }

        #endregion
    }
}
