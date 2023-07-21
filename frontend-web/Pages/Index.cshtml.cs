using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frontend_web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
            // promptText = "";
        }
        public async Task OnGet()
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage();
                // webapi is the container name
                // Run within network of docker
                //request.RequestUri = new Uri("http://webapi/Counter");
                request.RequestUri = new Uri(_configuration["backend-api-path"] + "/Image");
                // run via localhost
                var response = await client.SendAsync(request);
                string counter = await response.Content.ReadAsStringAsync();
                ViewData["Message"] = $"Counter value from cache :{counter}";
            }
        }
        // [BindProperty]
        // public string promptText { get; set; }
        public async Task<IActionResult> OnPostGenerateImage()
        {
            const string GEN_IMAGE_PATH = "/ImageGeneration";
            string prompt = Request.Form["promptText"];
            var response = await _httpClient.PostAsync(_configuration["backend-api-path"] + GEN_IMAGE_PATH + "?prompt=" + prompt, null);
            if (response.IsSuccessStatusCode)
            {
                //save image to disk
                var imageStream = await response.Content.ReadAsStreamAsync();
                string currentDirectory = Directory.GetCurrentDirectory() + "\\wwwroot";
                string directoryName = "Images";
                string directoryPath = Path.Combine(currentDirectory, directoryName);
                string fileName = "result.png";
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
                byte[] imageData;
                using (var memoryStream = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        await fs.CopyToAsync(memoryStream);
                    }
                    imageData = memoryStream.ToArray();
                }

                TempData["GeneratedImageUrl"] = "/Images/" + fileName;
            }
            return RedirectToPage("/Index");
        }
    }
}


