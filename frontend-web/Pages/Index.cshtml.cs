using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frontend_web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGet()
    {
        using (var client = new System.Net.Http.HttpClient())
        {
            // TODO: In real-world code, you shouldn't dispose HttpClient after every request. For best practices, see Use
            // https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            // Call *mywebapi*, and display its response in the page
            var request = new System.Net.Http.HttpRequestMessage();
            // webapi is the container name
            // Run within network of docker
            //request.RequestUri = new Uri("http://webapi/Counter");
            request.RequestUri = new Uri("http://localhost:5068/api/Image");
            // run via localhost
            var response = await client.SendAsync(request);
            string counter = await response.Content.ReadAsStringAsync();
            ViewData["Message"] = $"Counter value from cache :{counter}";
        }
    }
}

