using Microsoft.AspNetCore.Http;

namespace WebApp.Models
{
    public class UploadRequest
    {
        public string FileName { get; set; }
        public IFormFile File { get; set; }
    }
}
