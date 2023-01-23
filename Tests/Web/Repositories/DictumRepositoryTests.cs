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
[Collection(nameof(DictumRepository))]
[ExcludeFromCodeCoverage]
public class DictumRepositoryTests
{
    #region snippet_Properties

    private readonly DbContextOptions<EscortBookUserContext> _contextOptions;

    #endregion

    #region snippet_Constructors

    public DictumRepositoryTests()
        => _contextOptions = new DbContextOptionsBuilder<EscortBookUserContext>()
            .UseNpgsql(Postgres.ConnectionString)
            .Options;

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return empty enumerable when there are no rows")]
    public async Task GetAllAsyncShouldReturnEmptyEnumerable()
    {
        using var context = new EscortBookUserContext(_contextOptions);
        var dictumRepository = new DictumRepository(context);

        IEnumerable<Dictum> dictums = await dictumRepository.GetAllAsync(
            userId: "63cdc0ac9369794c59c10d47",
            page: 1,
            pageSize: 10
        );

        Assert.Empty(dictums);
    }

    #endregion
}
