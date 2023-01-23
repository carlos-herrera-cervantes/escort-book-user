using System.IO;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EscortBookUser.Web.Attributes;
using EscortBookUser.Web.Handlers;
using EscortBookUser.Web.Models;
using EscortBookUser.Web.Repositories;
using EscortBookUser.Web.Services;
using EscortBookUser.Web.Types;
using EscortBookUser.Web.Config;

namespace EscortBookUser.Web.Controllers;

[Route("api/v1/users")]
[ApiController]
public class AvatarController : ControllerBase
{
    #region snippet_Properties

    private readonly IAvatarRepository _avatarRepository;

    private readonly IAwsS3Service _s3Service;

    private readonly IOperationHandler<string> _operationHandler;

    #endregion

    #region snippet_Constructors

    public AvatarController(
        IAvatarRepository avatarRepository,
        IAwsS3Service s3Service,
        IOperationHandler<string> operationHandler
    )
    {
        _avatarRepository = avatarRepository;
        _s3Service = s3Service;
        _operationHandler = operationHandler;
    }

    #endregion

    #region snippet_ActionMethods

    [HttpGet("{id}/profile/avatar")]
    public async Task<IActionResult> GetByExternalAsync([FromRoute] string id)
    {
        Avatar avatar = await _avatarRepository.GetByIdAsync(id);

        if (avatar is null) return NotFound();

        avatar.Path = $"{S3.Endpoint}/{S3Buckets.UserProfile}/{avatar.Path}";

        return Ok(avatar);
    }

    [HttpGet("profile/avatar")]
    [ExcludeFromCodeCoverage]
    public async Task<IActionResult> GetByIdAsync([FromHeader(Name = "user-id")] string userId)
    {
        Avatar avatar = await _avatarRepository.GetByIdAsync(userId);

        if (avatar is null) return NotFound();

        avatar.Path = $"{S3.Endpoint}/{S3Buckets.UserProfile}/{avatar.Path}";

        return Ok(avatar);
    }

    [HttpPost("profile/avatar")]
    [ImageGuard]
    [RequestSizeLimit(2_000_000)]
    public async Task<IActionResult> CreateAsync(
        [FromForm] IFormFile image,
        [FromHeader(Name = "user-id")] string userId
    )
    {
        Stream imageStream = image.OpenReadStream();
        string url = await _s3Service.PutObjectAsync(image.FileName, userId, imageStream);

        var avatar = new Avatar
        {
            UserId = userId,
            Path = $"{userId}/{image.FileName}"
        };

        await _avatarRepository.CreateAsync(avatar);

        avatar.Path = url;

        return Created("", avatar);
    }

    [HttpPatch("profile/avatar")]
    [ImageGuard]
    [RequestSizeLimit(2_000_000)]
    public async Task<IActionResult> UpdateByIdAsync(
        [FromForm] IFormFile image,
        [FromHeader(Name = "user-id")] string userId
    )
    {
        Avatar avatar = await _avatarRepository.GetByIdAsync(userId);

        if (avatar is null) return NotFound();

        Stream imageStream = image.OpenReadStream();
        string url = await _s3Service.PutObjectAsync(image.FileName, userId, imageStream);

        Emitter<string>.EmitMessage(_operationHandler, avatar.Path);

        avatar.Path = $"{userId}/{image.FileName}";
        await _avatarRepository.UpdateByIdAsync(avatar);

        avatar.Path = url;

        return Ok(avatar);
    }

    [HttpDelete("profile/avatar")]
    public async Task<IActionResult> DeleteByIdAsync([FromHeader(Name = "user-id")] string userId)
    {
        Avatar avatar = await _avatarRepository.GetByIdAsync(userId);

        if (avatar is null) return NotFound();

        await _avatarRepository.DeleteByIdAsync(avatar.Id);
        Emitter<string>.EmitMessage(_operationHandler, avatar.Path);

        return NoContent();
    }

    #endregion
}
