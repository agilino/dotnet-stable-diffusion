using Microsoft.AspNetCore.Mvc;

namespace backend_api.Services
{
    public interface IImageGenerationService
    {
        Task<IActionResult> GenerateImage(string prompt);
        List<string> GetAllImages();
        byte[] GetOneImage(string imageName);
    }
}
