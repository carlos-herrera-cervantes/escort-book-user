using System.IO;
using System.Threading.Tasks;

namespace EscortBookUser.Web.Services;

public interface IAwsS3Service
{
    Task<string> PutObjectAsync(string key, string profileId, Stream imageStream);

    Task DeleteObjectAsync(string key);
}
