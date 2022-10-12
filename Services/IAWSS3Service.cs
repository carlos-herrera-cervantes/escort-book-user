using System.IO;
using System.Threading.Tasks;

namespace EscortBookUser.Services;

public interface IAWSS3Service
{
    Task<string> PutObjectAsync(string key, string profileId, Stream imageStream);

    Task DeleteObjectAsync(string key);
}
