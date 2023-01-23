using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using EscortBookUser.Web.Controllers;
using EscortBookUser.Web.Repositories;
using EscortBookUser.Web.Models;
using EscortBookUser.Web.Types;

namespace EscortBookUser.Tests.Web.Controllers;

[Category("Controllers")]
[Collection(nameof(StatusCategoryController))]
[ExcludeFromCodeCoverage]
public class StatusCategoryControllerTests
{
    #region snippet_Properties

    private readonly Mock<IStatusCategoryRepository> _mockStatusCategoryRepository;

    #endregion

    #region snippet_Constructors

    public StatusCategoryControllerTests()
        => _mockStatusCategoryRepository = new Mock<IStatusCategoryRepository>();

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return 200 status code")]
    public async Task GetAllAsyncShouldReturn200()
    {
        _mockStatusCategoryRepository
            .Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new List<StatusCategory>());

        var statusCategoryController = new StatusCategoryController(_mockStatusCategoryRepository.Object);

        IActionResult res = await statusCategoryController.GetAllAsync(pager: new Pager());

        _mockStatusCategoryRepository
            .Verify(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);

        Assert.IsType<OkObjectResult>(res);
    }

    #endregion
}
