using Microsoft.AspNetCore.Mvc;

namespace backend_api.Services
{
    public interface IImageGenerationService
    {
        Task<FileStreamResult> GenerateImage(string prompt);
        List<string> GetAllImages();
        byte[] GetOneImage(string imageName);
    }
}
