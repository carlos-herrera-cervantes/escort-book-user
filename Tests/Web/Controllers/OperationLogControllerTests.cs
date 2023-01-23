using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using EscortBookUser.Web.Controllers;
using EscortBookUser.Web.Repositories;
using EscortBookUser.Web.Types;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Tests.Web.Controllers;

[Category("Controllers")]
[Collection(nameof(OperationLogController))]
[ExcludeFromCodeCoverage]
public class OperationLogControllerTests
{
    #region snippet_Properties

    private readonly Mock<IRequestLogRepository> _mockRequestLogRepository;

    #endregion

    #region snippet_Constructors

    public OperationLogControllerTests() => _mockRequestLogRepository = new Mock<IRequestLogRepository>();

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return 200 status code")]
    public async Task GetAllAsyncShouldReturn200()
    {
        _mockRequestLogRepository
            .Setup(x => x.GetAllAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new List<RequestLog>());

        var operationLogController = new OperationLogController(_mockRequestLogRepository.Object);

        IActionResult res = await operationLogController.GetAllAsync(
            userid: "63cc84fb4a1dfa2b1667a834",
            pager: new Pager()
        );

        _mockRequestLogRepository.Verify(x => x.GetAllAsync(
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<int>()
        ), Times.Once);

        Assert.IsType<OkObjectResult>(res);
    }

    #endregion
}
