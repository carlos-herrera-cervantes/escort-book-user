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
[Collection(nameof(UserRepository))]
[ExcludeFromCodeCoverage]
public class UserRepositoryTests
{
    #region snippet_Properties

    private readonly DbContextOptions<EscortBookUserContext> _contextOptions;

    #endregion

    #region snippet_Constructors

    public UserRepositoryTests()
        => _contextOptions = new DbContextOptionsBuilder<EscortBookUserContext>()
            .UseNpgsql(Postgres.ConnectionString)
            .Options;

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return null when row does not exist")]
    public async Task GetByIdAsyncShouldReturnNull()
    {
        using var context = new EscortBookUserContext(_contextOptions);
        var userRepository = new UserRepository(context);

        User user = await userRepository.GetByIdAsync(userId: "63cdc0ac9369794c59c10d47");

        Assert.Null(user);
    }

    #endregion
}
