using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Xunit;
using EscortBookUser.Web.Repositories;
using EscortBookUser.Web.Contexts;
using EscortBookUser.Web.Config;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Tests.Web.Repositories;

[Category("Repositories")]
[Collection(nameof(RequestLogRepository))]
[ExcludeFromCodeCoverage]
public class RequestLogRepositoryTests
{
    #region snippet_Properties

    private readonly DbContextOptions<EscortBookUserContext> _contextOptions;

    #endregion

    #region snippet_Constructors

    public RequestLogRepositoryTests()
        => _contextOptions = new DbContextOptionsBuilder<EscortBookUserContext>()
            .UseNpgsql(Postgres.ConnectionString)
            .Options;

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return empty enumerable when there are no rows")]
    public async Task GetAllAsyncShouldReturnEmptyEnumerable()
    {
        using var context = new EscortBookUserContext(_contextOptions);
        var requestLogRepository = new RequestLogRepository(context);

        IEnumerable<RequestLog> logs = await requestLogRepository.GetAllAsync(
            userId: "63cdd9db714c22625781bd84",
            page: 1,
            pageSize: 10
        );

        Assert.Empty(logs);
    }

    #endregion
}
