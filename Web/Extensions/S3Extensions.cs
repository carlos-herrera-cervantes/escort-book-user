using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.DependencyInjection;
using EscortBookUser.Web.Config;

namespace EscortBookUser.Web.Extensions;

public static class S3Extensions
{
    public static IServiceCollection AddS3Client(this IServiceCollection services)
    {
        var awsCredentials = new BasicAWSCredentials(S3.AccessKey, S3.SecretKey);
        var s3Config = new AmazonS3Config
        {
            ServiceURL = S3.Endpoint,
            UseHttp = true,
            ForcePathStyle = true,
            AuthenticationRegion = S3.Region
        };
        var s3Client = new AmazonS3Client(awsCredentials, s3Config);

        services.AddSingleton<IAmazonS3>(_ => s3Client);

        return services;
    }
}
