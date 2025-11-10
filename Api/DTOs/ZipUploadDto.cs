using Microsoft.AspNetCore.Http;

namespace Api.DTOs
{
    public class ZipUploadDto
    {
        public IFormFile Zip { get; set; } = null!;
    }
}
