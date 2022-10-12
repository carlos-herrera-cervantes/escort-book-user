using System.Threading.Tasks;
using EscortBookUser.Attributes;
using EscortBookUser.Handlers;
using EscortBookUser.Models;
using EscortBookUser.Repositories;
using EscortBookUser.Services;
using EscortBookUser.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EscortBookUser.Controllers;

[Route("api/v1/users")]
[ApiController]
public class AvatarController : ControllerBase
{
    #region snippet_Properties

    private readonly IAvatarRepository _avatarRepository;

    private readonly IAWSS3Service _s3Service;

    private readonly IConfiguration _configuration;

    private readonly IOperationHandler<string> _operationHandler;

    #endregion

    #region snippet_Constructors

    public AvatarController
    (
        IAvatarRepository avatarRepository,
        IAWSS3Service s3Service,
        IConfiguration configuration,
        IOperationHandler<string> operationHandler
    )
    {
        _avatarRepository = avatarRepository;
        _s3Service = s3Service;
        _configuration = configuration;
        _operationHandler = operationHandler;
    }

    #endregion

    #region snippet_ActionMethods

    [HttpGet("{id}/profile/avatar")]
    public async Task<IActionResult> GetByExternalAsync([FromRoute] string id)
    {
        var avatar = await _avatarRepository.GetByIdAsync(id);

        if (avatar is null) return NotFound();

        var endpoint = _configuration["AWS:S3:Endpoint"];
        var bucketName = _configuration["AWS:S3:Name"];

        avatar.Path = $"{endpoint}/{bucketName}/{avatar.Path}";

        return Ok(avatar);
    }

    [HttpGet("profile/avatar")]
    public async Task<IActionResult> GetByIdAsync([FromHeader(Name = "user-id")] string userId)
    {
        var avatar = await _avatarRepository.GetByIdAsync(userId);

        if (avatar is null) return NotFound();

        var endpoint = _configuration["AWS:S3:Endpoint"];
        var bucketName = _configuration["AWS:S3:Name"];

        avatar.Path = $"{endpoint}/{bucketName}/{avatar.Path}";

        return Ok(avatar);
    }

    [HttpPost("profile/avatar")]
    [ImageGuard]
    [RequestSizeLimit(2_000_000)]
    public async Task<IActionResult> CreateAsync
    (
        [FromForm] IFormFile image,
        [FromHeader(Name = "user-id")] string userId
    )
    {
        var imageStream = image.OpenReadStream();
        var url = await _s3Service.PutObjectAsync(image.FileName, userId, imageStream);

        var avatar = new Avatar();
        avatar.UserId = userId;
        avatar.Path = $"{userId}/{image.FileName}";

        await _avatarRepository.CreateAsync(avatar);

        avatar.Path = url;

        return Created("", avatar);
    }

    [HttpPatch("profile/avatar")]
    [ImageGuard]
    [RequestSizeLimit(2_000_000)]
    public async Task<IActionResult> UpdateByIdAsync
    (
        [FromForm] IFormFile image,
        [FromHeader(Name = "user-id")] string userId
    )
    {
        var avatar = await _avatarRepository.GetByIdAsync(userId);

        if (avatar is null) return NotFound();

        var imageStream = image.OpenReadStream();
        var url = await _s3Service.PutObjectAsync(image.FileName, userId, imageStream);

        Emitter<string>.EmitMessage(_operationHandler, avatar.Path);

        avatar.Path = $"{userId}/{image.FileName}";
        await _avatarRepository.UpdateByIdAsync(avatar);

        avatar.Path = url;

        return Ok(avatar);
    }

    [HttpDelete("profile/avatar")]
    public async Task<IActionResult> DeleteByIdAsync([FromHeader(Name = "user-id")] string userId)
    {
        var avatar = await _avatarRepository.GetByIdAsync(userId);

        if (avatar is null) return NotFound();

        await _avatarRepository.DeleteByIdAsync(avatar.Id);
        Emitter<string>.EmitMessage(_operationHandler, avatar.Path);

        return NoContent();
    }

    #endregion
}
