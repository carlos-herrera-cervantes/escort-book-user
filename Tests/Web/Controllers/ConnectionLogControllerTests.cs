using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using EscortBookUser.Web.Controllers;
using EscortBookUser.Web.Repositories;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Tests.Web.Controllers;

[Category("Controllers")]
[Collection(nameof(ConnectionLogController))]
[ExcludeFromCodeCoverage]
public class ConnectionLogControllerTests
{
    #region snippet_Properties

    private readonly Mock<IConnectionLogRepository> _mockConnectionLogRepository;

    #endregion

    #region snippet_Constructors

    public ConnectionLogControllerTests()
        => _mockConnectionLogRepository = new Mock<IConnectionLogRepository>();

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return 404 status code")]
    public async Task GetByIdAsyncShouldReturn404()
    {
        _mockConnectionLogRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(() => null);

        var connectionLogController = new ConnectionLogController(_mockConnectionLogRepository.Object);

        IActionResult res = await connectionLogController.GetByIdAsync(id: "63cc7f66c48125344e188adb");

        _mockConnectionLogRepository.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);

        Assert.IsType<NotFoundResult>(res);
    }

    [Fact(DisplayName = "Should return 200 status code")]
    public async Task GetByIdAsyncShouldReturn200()
    {
        _mockConnectionLogRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(new ConnectionLog());

        var connectionLogController = new ConnectionLogController(_mockConnectionLogRepository.Object);

        IActionResult res = await connectionLogController.GetByIdAsync(id: "63cc7f66c48125344e188adb");

        _mockConnectionLogRepository.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Once);

        Assert.IsType<OkObjectResult>(res);
    }

    #endregion
}
