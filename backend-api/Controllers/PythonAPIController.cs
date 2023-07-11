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
                httpClient.Timeout = TimeSpan.FromMinutes(30);
                string apiUrl = "http://localhost:8000/generate?prompt=" + Uri.EscapeDataString(prompt);

                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var imageStream = await response.Content.ReadAsStreamAsync();
                    string filePath = "D:/Agilino/Projects/dotnet-stable-diffusion/backend-api/Images/image.png";
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        await imageStream.CopyToAsync(fs);
                    }
                    return File(imageStream, "image/png");
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }
        }
    }
}
