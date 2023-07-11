using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PythonAPIController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> CallPythonAPI([FromQuery] string prompt)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);
                string apiUrl = "http://localhost:8000/generate?prompt=" + Uri.EscapeDataString(prompt);

                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var imageStream = await response.Content.ReadAsStreamAsync();
                    string currentDirectory = Directory.GetCurrentDirectory();
                    string directoryName = "Images";
                    string directoryPath = Path.Combine(currentDirectory, directoryName);

                    string fileName = "image.png";
                    string filePath = Path.Combine(directoryPath, fileName);

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        await imageStream.CopyToAsync(fs);
                    }

                    imageStream.Dispose();

                    return File(filePath, "image/png");
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }
        }
    }
}
