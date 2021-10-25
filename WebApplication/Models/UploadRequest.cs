using Microsoft.AspNetCore.Http;

namespace WebApplication.Models
{
    public class UploadRequest
    {
        public string FileName { get; set; }
        public IFormFile File { get; set; }
    }
}
