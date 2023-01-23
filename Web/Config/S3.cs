using System;

namespace EscortBookUser.Web.Config;

public static class S3
{
    public static readonly string AccessKey = Environment.GetEnvironmentVariable("AWS_S3_ACCESS_KEY");

    public static readonly string SecretKey = Environment.GetEnvironmentVariable("AWS_S3_SECRET_KEY");

    public static readonly string Endpoint = Environment.GetEnvironmentVariable("AWS_S3_ENDPOINT");

    public static readonly string Region = Environment.GetEnvironmentVariable("AWS_S3_REGION");
}

public static class S3Buckets
{
    public static readonly string UserProfile = Environment.GetEnvironmentVariable("AWS_S3_NAME");
}
