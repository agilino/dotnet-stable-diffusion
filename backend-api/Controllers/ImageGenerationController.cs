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

        /*------------------------------------------------

        //TODO: Create an endpoint using GenerateImage service and return an image name
        Hint: Receive a string input name "prompt" from client and use as a Post moethod

        

        -----------------------------------------------*/

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
