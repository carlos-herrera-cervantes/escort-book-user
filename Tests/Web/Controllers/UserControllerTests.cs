using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Xunit;
using Moq;
using EscortBookUser.Web.Controllers;
using EscortBookUser.Web.Repositories;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Tests.Web.Controllers;

[Category("Controllers")]
[Collection(nameof(UserController))]
[ExcludeFromCodeCoverage]
public class UserControllerTests
{
    #region snippet_Properties

    private readonly Mock<IUserRepository> _mockUserRepository;

    #endregion

    #region snippet_Constructors

    public UserControllerTests() => _mockUserRepository = new Mock<IUserRepository>();

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return 404 status code")]
    public async Task GetByIdAsyncShouldReturn404()
    {
        _mockUserRepository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(() => null);

        var userController = new UserController(_mockUserRepository.Object);

        IActionResult res = await userController.GetByIdAsync(userId: "63cdc0ac9369794c59c10d47");

        _mockUserRepository.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);

        Assert.IsType<NotFoundResult>(res);
    }

    [Fact(DisplayName = "Should return 200 status code")]
    public async Task GetByIdAsyncShouldReturn200()
    {
        _mockUserRepository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(new User());

        var userController = new UserController(_mockUserRepository.Object);

        IActionResult res = await userController.GetByIdAsync(userId: "63cdc0ac9369794c59c10d47");

        _mockUserRepository.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);

        Assert.IsType<OkObjectResult>(res);
    }

    [Fact(DisplayName = "Should return 404 status code")]
    public async Task UpdateByIdAsyncShouldReturn404()
    {
        _mockUserRepository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(() => null);

        var userController = new UserController(_mockUserRepository.Object);

        IActionResult res = await userController.UpdateByIdAsync(
            userId: "63cdc0ac9369794c59c10d47",
            partialUser: new JsonPatchDocument<User>()
        );

        _mockUserRepository.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
        _mockUserRepository.Verify(x => x.UpdateByIdAsync(
            It.IsAny<string>(),
            It.IsAny<User>(),
            It.IsAny<JsonPatchDocument<User>>()
        ), Times.Never);

        Assert.IsType<NotFoundResult>(res);
    }

    [Fact(DisplayName = "Should return 200 status code")]
    public async Task UpdateByIdAsyncShouldReturn200()
    {
        _mockUserRepository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(new User());
        _mockUserRepository.Setup(x => x.UpdateByIdAsync(
            It.IsAny<string>(),
            It.IsAny<User>(),
            It.IsAny<JsonPatchDocument<User>>()
        )).Returns(Task.CompletedTask);

        var userController = new UserController(_mockUserRepository.Object);

        IActionResult res = await userController.UpdateByIdAsync(
            userId: "63cdc0ac9369794c59c10d47",
            partialUser: new JsonPatchDocument<User>()
        );

        _mockUserRepository.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);
        _mockUserRepository.Verify(x => x.UpdateByIdAsync(
            It.IsAny<string>(),
            It.IsAny<User>(),
            It.IsAny<JsonPatchDocument<User>>()
        ), Times.Once);

        Assert.IsType<OkObjectResult>(res);
    }

    #endregion
}
