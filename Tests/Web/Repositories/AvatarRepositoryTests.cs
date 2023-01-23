using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using EscortBookUser.Web.Repositories;
using EscortBookUser.Web.Contexts;
using EscortBookUser.Web.Config;
using EscortBookUser.Web.Models;

namespace EscortBookUser.Tests.Web.Repositories;

[Category("Repositories")]
[Collection(nameof(AvatarRepository))]
[ExcludeFromCodeCoverage]
public class AvatarRepositoryTests
{
    #region snippet_Properties

    private readonly DbContextOptions<EscortBookUserContext> _contextOptions;

    #endregion

    #region snippet_Constructors

    public AvatarRepositoryTests()
        => _contextOptions = new DbContextOptionsBuilder<EscortBookUserContext>()
            .UseNpgsql(Postgres.ConnectionString)
            .Options;

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return null when row does not exists")]
    public async Task GetByIdAsyncShouldReturnNull()
    {
        using var context = new EscortBookUserContext(_contextOptions);
        var avatarRepository = new AvatarRepository(context);

        Avatar avatar = await avatarRepository.GetByIdAsync(userId: "63cdc0ac9369794c59c10d47");

        Assert.Null(avatar);
    }

    #endregion
}
