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
        public async Task<FileStreamResult> GeneratePNGImageAsync(string prompt)
        {
            var imageBytes = await _imageGenerationService.GenerateImage(prompt);
            return imageBytes;
        }

        [HttpGet("gallery")]
        public IActionResult GetGalleryImages()
        {
            List<string> imageUrls = _imageGenerationService.GetAllImages();
            return Ok(imageUrls);
        }

        [HttpGet("image/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            byte[] imageBytes = _imageGenerationService.GetOneImage(imageName);
            if (imageBytes == null)
            {
                return NotFound();
            }

            return File(imageBytes, "image/png");
        }
    }
}
