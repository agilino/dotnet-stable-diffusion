using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace backend_api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IDistributedCache _cache;

        public ImageController(ILogger<ImageController> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpGet(Name = "GetCounter")]
        public string get() {
            string key = "Counter";
            string? result = null;
            try
            {
                var counterStr = _cache.GetString(key);
                if (int.TryParse(counterStr, out int counter))
                {
                    counter++;
                }
                else
                {
                    counter = 0;
                }
                result = counter.ToString();
                _cache.SetString(key, result);
            }
            catch (RedisConnectionException)
            {
                result = "Redis cache is not found.";
            }
            return result;
        }
    }
}
