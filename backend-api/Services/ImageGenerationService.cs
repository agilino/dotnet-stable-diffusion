﻿using backend_api.Entities;
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
        public async Task<string> GenerateImage(string prompt)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);
                string apiUrl = _configuration["ApiUrl"] + "?prompt=" + Uri.EscapeDataString(prompt);

                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var imageStream = await response.Content.ReadAsStreamAsync();
                    string currentDirectory = Directory.GetCurrentDirectory();
                    string directoryName = "Images";
                    string directoryPath = Path.Combine(currentDirectory, directoryName);

                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                    string filePath = Path.Combine(directoryPath, fileName);

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        await imageStream.CopyToAsync(fs);
                    }
                    return fileName;
                }
                else
                {
                    return "";
                }
            }
        }

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
