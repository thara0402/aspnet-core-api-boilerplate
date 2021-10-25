using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WebApplication.Infrastructure
{
    public interface IFileRepository
    {
        Task UploadAsync(Stream fileStream, string fileName, string contentType);

        Task<byte[]> DownloadAsync(string fileName);

        Task<IList<string>> GetAsync();
    }
}
