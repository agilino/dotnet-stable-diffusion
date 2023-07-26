using Microsoft.AspNetCore.Mvc;

namespace backend_api.Services
{
    public interface IImageGenerationService
    {
        Task<string> GenerateImage(string prompt);
        IActionResult DownloadImage(string imageName);
        List<string> GetAllImages();
        byte[] GetOneImage(string imageName);
    }
}
