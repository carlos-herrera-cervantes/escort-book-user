using System.IO;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using EscortBookUser.Web.Controllers;
using EscortBookUser.Web.Repositories;
using EscortBookUser.Web.Services;
using EscortBookUser.Web.Handlers;
using EscortBookUser.Web.Models;
using EscortBookUser.Web.Config;

namespace EscortBookUser.Tests.Web.Controllers;

[Category("Controllers")]
[Collection(nameof(AvatarController))]
[ExcludeFromCodeCoverage]
public class AvatarControllerTests
{
    #region snippet_Properties

    private readonly Mock<IAvatarRepository> _mockAvatarRepository;

    private readonly Mock<IAwsS3Service> _mockAwsS3Service;

    private readonly Mock<IOperationHandler<string>> _mockOperationHandler;

    private readonly Mock<IFormFile> _mockFormFile;

    #endregion

    #region snippet_Constructors

    public AvatarControllerTests()
    {
        _mockAvatarRepository = new Mock<IAvatarRepository>();
        _mockAwsS3Service = new Mock<IAwsS3Service>();
        _mockOperationHandler = new Mock<IOperationHandler<string>>();
        _mockFormFile = new Mock<IFormFile>();
    }

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return 404 status code")]
    public async Task GetByExternalAsyncShouldReturn404()
    {
        _mockAvatarRepository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(() => null);

        var avatarController = new AvatarController(
            _mockAvatarRepository.Object,
            _mockAwsS3Service.Object,
            _mockOperationHandler.Object
        );

        IActionResult res = await avatarController.GetByExternalAsync(id: "6382727e12a820ffa6932870");

        _mockAvatarRepository.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);

        Assert.IsType<NotFoundResult>(res);
    }

    [Fact(DisplayName = "Should return 200 status code")]
    public async Task GetByExternalAsyncShouldReturn200()
    {
        _mockAvatarRepository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(new Avatar
        {
            Path = "6382727e12a820ffa6932870/profile.png"
        });

        var avatarController = new AvatarController(
            _mockAvatarRepository.Object,
            _mockAwsS3Service.Object,
            _mockOperationHandler.Object
        );

        IActionResult res = await avatarController.GetByExternalAsync(id: "6382727e12a820ffa6932870");

        _mockAvatarRepository.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);

        Assert.IsType<OkObjectResult>(res);

        var expectedPath = $"{S3.Endpoint}/{S3Buckets.UserProfile}/6382727e12a820ffa6932870/profile.png";
        var okObjectResult = res as OkObjectResult;
        var avatar = okObjectResult?.Value as Avatar;

        Assert.True(avatar?.Path == expectedPath);
    }

    [Fact(DisplayName = "Should return 201 status code")]
    public async Task CreateAsyncShouldReturn201()
    {
        _mockAwsS3Service
            .Setup(x => x.PutObjectAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Stream>()))
            .ReturnsAsync($"{S3.Endpoint}/{S3Buckets.UserProfile}/6382727e12a820ffa6932870/profile.png");
        _mockAvatarRepository.Setup(x => x.CreateAsync(It.IsAny<Avatar>())).Returns(Task.CompletedTask);
        _mockFormFile.Setup(x => x.FileName).Returns("profile.png");

        var avatarController = new AvatarController(
            _mockAvatarRepository.Object,
            _mockAwsS3Service.Object,
            _mockOperationHandler.Object
        );

        IActionResult res = await avatarController.CreateAsync(
            image: _mockFormFile.Object,
            userId: "6382727e12a820ffa6932870"
        );

        _mockAwsS3Service.Verify(x => x.PutObjectAsync(
            It.IsAny<string>(),
            It.IsAny<string>(), It.IsAny<Stream>()
        ), Times.Once);
        _mockAvatarRepository.Verify(x => x.CreateAsync(It.IsAny<Avatar>()), Times.Once);

        Assert.IsType<CreatedResult>(res);

        var expectedPath = $"{S3.Endpoint}/{S3Buckets.UserProfile}/6382727e12a820ffa6932870/profile.png";
        var createdResult = res as CreatedResult;
        var avatar = createdResult?.Value as Avatar;

        Assert.True(avatar?.Path == expectedPath);
    }

    [Fact(DisplayName = "Should return 404 status code")]
    public async Task UpdateByIdAsyncShouldReturn404()
    {
        _mockAvatarRepository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(() => null);

        var avatarController = new AvatarController(
            _mockAvatarRepository.Object,
            _mockAwsS3Service.Object,
            _mockOperationHandler.Object
        );

        IActionResult res = await avatarController.UpdateByIdAsync(
            image: _mockFormFile.Object,
            userId: "6382727e12a820ffa6932870"
        );

        _mockAvatarRepository.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);

        Assert.IsType<NotFoundResult>(res);
    }

    [Fact(DisplayName = "Should return 200 status code")]
    public async Task UpdateByIdAsyncShouldReturn200()
    {
        _mockAvatarRepository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(new Avatar());

        var expectedPath = $"{S3.Endpoint}/{S3Buckets.UserProfile}/6382727e12a820ffa6932870/profile.png";
        _mockAwsS3Service
            .Setup(x => x.PutObjectAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Stream>()))
            .ReturnsAsync(expectedPath);

        _mockOperationHandler.Setup(x => x.Publish(It.IsAny<string>()));
        _mockFormFile.Setup(x => x.FileName).Returns("profile.png");
        _mockAvatarRepository.Setup(x => x.UpdateByIdAsync(It.IsAny<Avatar>())).Returns(Task.CompletedTask);

        var avatarController = new AvatarController(
            _mockAvatarRepository.Object,
            _mockAwsS3Service.Object,
            _mockOperationHandler.Object
        );

        IActionResult res = await avatarController.UpdateByIdAsync(
            image: _mockFormFile.Object,
            userId: "6382727e12a820ffa6932870"
        );

        _mockAvatarRepository.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
        _mockAwsS3Service.Verify(x => x.PutObjectAsync(
            It.IsAny<string>(),
            It.IsAny<string>(), It.IsAny<Stream>()
        ), Times.Once);
        _mockOperationHandler.Verify(x => x.Publish(It.IsAny<string>()), Times.Once);
        _mockAvatarRepository.Verify(x => x.UpdateByIdAsync(It.IsAny<Avatar>()), Times.Once);

        Assert.IsType<OkObjectResult>(res);

        var okObjectResult = res as OkObjectResult;
        var avatar = okObjectResult?.Value as Avatar;

        Assert.True(avatar?.Path == expectedPath);
    }

    [Fact(DisplayName = "Should return 404 status code")]
    public async Task DeleteByIdAsyncShouldReturn404()
    {
        _mockAvatarRepository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(() => null);

        var avatarController = new AvatarController(
            _mockAvatarRepository.Object,
            _mockAwsS3Service.Object,
            _mockOperationHandler.Object
        );

        IActionResult res = await avatarController.DeleteByIdAsync(userId: "6382727e12a820ffa6932870");

        _mockAvatarRepository.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);

        Assert.IsType<NotFoundResult>(res);
    }

    [Fact(DisplayName = "Should return 204 status code")]
    public async Task DeleteByIdAsyncShouldReturn204()
    {
        _mockAvatarRepository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(new Avatar());
        _mockAvatarRepository.Setup(x => x.DeleteByIdAsync(It.IsAny<string>())).Returns(Task.CompletedTask);
        _mockOperationHandler.Setup(x => x.Publish(It.IsAny<string>()));

        var avatarController = new AvatarController(
            _mockAvatarRepository.Object,
            _mockAwsS3Service.Object,
            _mockOperationHandler.Object
        );

        IActionResult res = await avatarController.DeleteByIdAsync(userId: "6382727e12a820ffa6932870");

        _mockAvatarRepository.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
        _mockAvatarRepository.Verify(x => x.DeleteByIdAsync(It.IsAny<string>()), Times.Once);
        _mockOperationHandler.Verify(x => x.Publish(It.IsAny<string>()), Times.Once);

        Assert.IsType<NoContentResult>(res);
    }

    #endregion
}
