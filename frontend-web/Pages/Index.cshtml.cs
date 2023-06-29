using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frontend_web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    readonly IConfiguration _configuration;

    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task OnGet()
    {
        using (var client = new HttpClient())
        {
            // TODO: In real-world code, you shouldn't dispose HttpClient after every request. For best practices, see Use
            // https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            // Call *mywebapi*, and display its response in the page
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
}

