using backend_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageGenerationController : Controller
    {
        private readonly IImageGenerationService _imageGenerationService;
        public ImageGenerationController(IImageGenerationService imageGenerationService)
        {
            _imageGenerationService = imageGenerationService;
        }

        [HttpGet("{prompt}")]
        public async Task<string> GeneratePNGImage(string prompt)
        {
            var imageBytes = await _imageGenerationService.GenerateImage(prompt);
            return imageBytes;
        }

        [HttpGet("download/{image-name}")]
        public IActionResult DownloadImage([FromRoute(Name = "image-name")] string imageName)
        {
            return _imageGenerationService.DownloadImage(imageName);
        }

        [HttpGet("gallery")]
        public IActionResult GetGalleryImages()
        {
            List<string> imageUrls = _imageGenerationService.GetAllImages();
            return Ok(imageUrls);
        }

        [HttpGet("image/{image-name}")]
        public IActionResult GetImage([FromRoute(Name = "image-name")] string imageName)
        {
            byte[] imageBytes = _imageGenerationService.GetOneImage(imageName);
            return File(imageBytes, "image/png");
        }
    }
}
