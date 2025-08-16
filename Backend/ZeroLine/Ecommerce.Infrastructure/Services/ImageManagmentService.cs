
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using ZeroLine.API.Services;

namespace ZeroLine.Infrastructure.Services
{
    public class ImageManagmentService : IImageManagmentService
    {
        private readonly IFileProvider _fileProvider;
        public ImageManagmentService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;

        }
        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            List<string> SaveImageSrc = new List<string>();
            var ImageDirectory = Path.Combine("wwwroot", "images", src);
            if (!Directory.Exists(ImageDirectory))
            {
                Directory.CreateDirectory(ImageDirectory);
            }
            foreach (var item in files)
            {
                var ImageName = item.FileName;
                var ImageSrc = Path.Combine($"/Images/{src}/{ImageName}");
                var root = Path.Combine(ImageDirectory, ImageName);
                using (FileStream stream = new FileStream(root, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }
                SaveImageSrc.Add(ImageSrc);
            }
            return SaveImageSrc;
        }

        public async Task DeleteImageAsync(string src)
        {
            var info = _fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            await Task.Run(() => File.Delete(root));

        }
    }
}
