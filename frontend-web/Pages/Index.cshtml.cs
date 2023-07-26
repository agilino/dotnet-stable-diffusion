using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text;

namespace frontend_web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public List<string> ImageUrls { get; set; }

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task OnGetAsync()
        {
            ImageUrls = await GetImageUrlsFromApi();
        }
        private async Task<List<string>> GetImageUrlsFromApi()
        {
            var httpClient = _httpClientFactory.CreateClient();

            string apiUrl = "http://localhost:5068/api/ImageGeneration/gallery";

            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var imageUrls = JsonConvert.DeserializeObject<List<string>>(content);
                return imageUrls;
            }

            return new List<string>();
        }
        public async Task<IActionResult> OnPostGenerateImage()
        {
            string prompt = Request.Form["PromptText"];
            var httpClient = _httpClientFactory.CreateClient();

            string apiUrl = "http://localhost:5068/api/ImageGeneration";
            string jsonRequest = JsonConvert.SerializeObject(Uri.EscapeDataString(prompt));
            HttpContent httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(apiUrl, httpContent);
            if (response.IsSuccessStatusCode)
            {
                if(response.Content != null)
                {
                    TempData["ImageName"] = await response.Content.ReadAsStringAsync();
                }
            }
            return Redirect("/Index");
        }
    }
}