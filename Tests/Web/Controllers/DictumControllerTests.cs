using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using EscortBookUser.Web.Controllers;
using EscortBookUser.Web.Repositories;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Tests.Web.Controllers;

[Category("Controllers")]
[Collection(nameof(DictumController))]
[ExcludeFromCodeCoverage]
public class DictumControllerTests
{
    #region snippet_Properties

    private readonly Mock<IDictumRepository> _mockDictumRepository;

    #endregion

    #region snippet_Constructors

    public DictumControllerTests() => _mockDictumRepository = new Mock<IDictumRepository>();

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return 200 status code")]
    public async Task GetAllShouldReturn200()
    {
        _mockDictumRepository
            .Setup(x => x.GetAllAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new List<Dictum>());

        var dictumController = new DictumController(_mockDictumRepository.Object);

        IActionResult res = await dictumController.GetAllAsync(userId: "63cc82dcb1413f2f9973aaf0");

        _mockDictumRepository.Verify(x => x.GetAllAsync(
            It.IsAny<string>(),
            It.IsAny<int>(), It.IsAny<int>()
        ), Times.Once);

        Assert.IsType<OkObjectResult>(res);
    }

    [Fact(DisplayName = "Should return 201 status code")]
    public async Task CreateAsyncShouldReturn201()
    {
        _mockDictumRepository.Setup(x => x.CreateAsync(It.IsAny<Dictum>())).Returns(Task.CompletedTask);

        var dictumController = new DictumController(_mockDictumRepository.Object);

        IActionResult res = await dictumController.CreateAsync(
            userId: "63cc82dcb1413f2f9973aaf0",
            id: "63cc84fb4a1dfa2b1667a834",
            dictum: new Dictum()
        );

        _mockDictumRepository.Verify(x => x.CreateAsync(It.IsAny<Dictum>()), Times.Once);

        Assert.IsType<CreatedResult>(res);
    }

    [Fact(DisplayName = "Should return 204 status code")]
    public async Task DeleteByIdAsyncShouldReturn204()
    {
        _mockDictumRepository.Setup(x => x.DeleteByIdAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

        var dictumController = new DictumController(_mockDictumRepository.Object);

        IActionResult res = await dictumController.DeleteByIdAsync(
            userId: "63cc82dcb1413f2f9973aaf0",
            dictumId: "63cc84fb4a1dfa2b1667a834"
        );

        _mockDictumRepository.Verify(x => x.DeleteByIdAsync(It.IsAny<string>()), Times.Once);

        Assert.IsType<NoContentResult>(res);
    }

    #endregion
}
