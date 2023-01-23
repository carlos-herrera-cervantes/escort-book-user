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
[Collection(nameof(StatusCategoryRepository))]
[ExcludeFromCodeCoverage]
public class StatusCategoryRepositoryTests
{
    #region snippet_Properties

    private readonly DbContextOptions<EscortBookUserContext> _contextOptions;

    #endregion

    #region snippet_Constructors

    public StatusCategoryRepositoryTests()
        => _contextOptions = new DbContextOptionsBuilder<EscortBookUserContext>()
            .UseNpgsql(Postgres.ConnectionString)
            .Options;

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return enumerable")]
    public async Task GetAllAsyncShouldReturnEnumerable()
    {
        using var context = new EscortBookUserContext(_contextOptions);
        var statusCategoryRepository = new StatusCategoryRepository(context);

        IEnumerable<StatusCategory> categories = await statusCategoryRepository.GetAllAsync(page: 1, pageSize: 10);

        Assert.NotEmpty(categories);
    }

    #endregion
}
