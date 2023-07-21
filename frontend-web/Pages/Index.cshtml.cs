using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
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
        public async Task<IActionResult> OnPostGenerateImage()
        {
            // const string GEN_IMAGE_PATH = "/ImageGeneration/";
            string prompt = Request.Form["PromptText"];
            // var response = await _httpClient.PostAsync(_configuration["backend-api-path"] + GEN_IMAGE_PATH + "?prompt=" + prompt, null);
            // if (response.IsSuccessStatusCode)
            // {
            //     var imageStream =  await response.Content.ReadAsStreamAsync();
            //     imageStream.ReadByte();
            //     TempData["ImageUrl"] = imageStream;
            // }
            TempData["ImageUrl"] = prompt;
            return RedirectToPage("/Index");
        }
    }
}