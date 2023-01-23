using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Xunit;
using Moq;
using Amazon.S3;
using Amazon.S3.Model;
using EscortBookUser.Web.Services;
using EscortBookUser.Web.Config;

namespace EscortBookUser.Tests.Web.Services;

[Category("Services")]
[Collection(nameof(AwsS3Service))]
[ExcludeFromCodeCoverage]
public class AwsS3ServiceTests
{
    #region snippet_Properties

    private readonly Mock<IAmazonS3> _mockAmazonS3;

    private readonly Mock<IFormFile> _mockFormFile;

    #endregion

    #region snippet_Constructors

    public AwsS3ServiceTests()
    {
        _mockAmazonS3 = new Mock<IAmazonS3>();
        _mockFormFile = new Mock<IFormFile>();
    }

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should return the URL with full path to the file")]
    public async Task PutObjectAsyncShouldReturnString()
    {
        _mockAmazonS3
            .Setup(x => x.PutObjectAsync(
                It.IsAny<PutObjectRequest>(),
                It.IsAny<CancellationToken>()
            ));

        var s3Service = new AwsS3Service(_mockAmazonS3.Object);

        string url = await s3Service.PutObjectAsync(
            key: "profile.png",
            profileId: "63850d910b43fadcaedcd106",
            imageStream: _mockFormFile.Object.OpenReadStream()
        );

        _mockAmazonS3.Verify(x => x.PutObjectAsync(
            It.IsAny<PutObjectRequest>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);

        var expectedUrl = $"{S3.Endpoint}/{S3Buckets.UserProfile}" +
            "/63850d910b43fadcaedcd106/profile.png";

        Assert.True(url == expectedUrl);
    }

    [Fact(DisplayName = "Should successfully invoke DeleteObjectAsync method of the service")]
    public async Task DeleteObjectAsyncShouldBeSuccessfully()
    {
        _mockAmazonS3
            .Setup(x => x.DeleteObjectAsync(
                It.IsAny<DeleteObjectRequest>(),
                It.IsAny<CancellationToken>()
            ));

        var s3Service = new AwsS3Service(_mockAmazonS3.Object);
        await s3Service.DeleteObjectAsync(key: "profile.png");

        _mockAmazonS3.Verify(x => x.DeleteObjectAsync(
            It.IsAny<DeleteObjectRequest>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

    #endregion
}
