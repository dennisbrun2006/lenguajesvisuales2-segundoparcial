using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
namespace Api.Infrastructure.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;
        public LocalFileStorageService(IWebHostEnvironment env) => _env = env;

        private static string Sanitize(string name) =>
            System.Text.RegularExpressions.Regex.Replace(name, @"[^\w\.-]", "_");

        private string WebRoot() => _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        public async Task<string> SaveFileAsync(IFormFile file, string relativeFolder)
        {
            var root = WebRoot();
            var dest = Path.Combine(root, relativeFolder.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            Directory.CreateDirectory(dest);

            var safe = $"{Guid.NewGuid():N}_{Sanitize(file.FileName)}";
            var path = Path.Combine(dest, safe);
            using var fs = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fs);

            var url = Path.Combine(relativeFolder, safe).Replace("\\", "/");
            return url.StartsWith("/") ? url : "/" + url;
        }

        public async Task<string[]> SaveZipAndExtractAsync(IFormFile zipFile, string relativeFolder)
        {
            var root = WebRoot();
            var dest = Path.Combine(root, relativeFolder.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            Directory.CreateDirectory(dest);

            var tempDir = Path.Combine(Path.GetTempPath(), "zip_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempDir);
            var tempZip = Path.Combine(tempDir, Sanitize(zipFile.FileName));
            using (var fs = new FileStream(tempZip, FileMode.Create)) await zipFile.CopyToAsync(fs);

            ZipFile.ExtractToDirectory(tempZip, dest, overwriteFiles: true);

            var files = Directory.GetFiles(dest, "*", SearchOption.AllDirectories);
            var urls = files.Select(f => f.Replace(root, "").Replace("\\", "/")).ToArray();
            try { File.Delete(tempZip); Directory.Delete(tempDir, true); } catch { }

            for (int i = 0; i < urls.Length; i++) if (!urls[i].StartsWith("/")) urls[i] = "/" + urls[i];
            return urls;
        }
    }
}
