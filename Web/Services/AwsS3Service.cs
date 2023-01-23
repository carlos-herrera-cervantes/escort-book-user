using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using EscortBookUser.Web.Config;

namespace EscortBookUser.Web.Services;

public class AwsS3Service : IAwsS3Service
{
    #region snippet_Properties

    private readonly IAmazonS3 _s3Client;

    #endregion

    #region snippet_Constructors

    public AwsS3Service(IAmazonS3 s3Client) => _s3Client = s3Client;

    #endregion

    #region snippet_ActionMethods

    public async Task<string> PutObjectAsync(string key, string profileId, Stream imageStream)
    {
        var request = new PutObjectRequest
        {
            InputStream = imageStream,
            BucketName = S3Buckets.UserProfile,
            Key = $"{profileId}/{key}"
        };

        await _s3Client.PutObjectAsync(request);
        return $"{S3.Endpoint}/{S3Buckets.UserProfile}/{profileId}/{key}";
    }

    public async Task DeleteObjectAsync(string key)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = S3Buckets.UserProfile,
            Key = key
        };
        await _s3Client.DeleteObjectAsync(request);
    }

    #endregion
}
