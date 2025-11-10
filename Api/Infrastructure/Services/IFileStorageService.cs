using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Api.Infrastructure.Services
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, string relativeFolder);      // => "/uploads/CI/..."
        Task<string[]> SaveZipAndExtractAsync(IFormFile zipFile, string relativeFolder); // => ["/uploads/CI/doc1.png", ...]
    }
}
