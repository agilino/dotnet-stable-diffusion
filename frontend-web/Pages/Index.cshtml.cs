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
            return await Task.FromResult(new List<string>());
            /*---------------------------------
            //TODO: Connect to back-end API and return list image urls
            Given back-end URL: "http://localhost:5068/api/ImageGeneration/gallery"
            Hint: Call API and convert response to get list image urls
            Return type: List<string>
            ----------------------------------*/
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
                if (response.Content != null)
                {
                    TempData["ImageName"] = await response.Content.ReadAsStringAsync();
                }
            }
            return Redirect("/Index");
        }
    }
}