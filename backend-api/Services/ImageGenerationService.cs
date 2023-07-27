using Microsoft.AspNetCore.Mvc;

namespace backend_api.Services
{
    public class ImageGenerationService : IImageGenerationService
    {
        private const int STREAM_FIRST_POSITION = 0;
        private readonly IConfiguration _configuration;
        private readonly string _imagesDirectory;
        public ImageGenerationService(IConfiguration configuration)
        {
            _configuration = configuration;
            _imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Images");
        }
        /*------------------------------------------------*/
        //GenerateImage Service
        //TODO: Call stable diffusion api to generate an image from input prompt
        //and return an image name after stored.
        //Steps to do:
        //Step 1: Connect to stable diffusion api
        //Step 2: Store the returned image to Images folder in Directory
        //Step 3: Return the image name.

        /*-----------------------------------------------*/

        public IActionResult DownloadImage(string imageName)
        {
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            string filePath = Path.Combine(directoryPath, imageName);

            if (File.Exists(filePath))
            {
                string contentType = "image/png";
                return new PhysicalFileResult(filePath, contentType)
                {
                    FileDownloadName = Path.GetFileName(filePath)
                };
            }
            else
            {
                return new NotFoundResult();
            }
        }

        public List<string> GetAllImages()
        {
            string[] imageFiles = Directory.GetFiles(_imagesDirectory);

            List<string> imageUrls = new List<string>();

            foreach (string imageFile in imageFiles)
            {
                string imageName = Path.GetFileName(imageFile);
                string imageUrl = $"{_configuration["ImageUrl"]}{imageName}";
                imageUrls.Add(imageUrl);
            }

            return imageUrls;
        }

        public byte[] GetOneImage(string imageName)
        {
            string imagePath = Path.Combine(_imagesDirectory, imageName);
            return File.ReadAllBytes(imagePath);
        }
    }
}
