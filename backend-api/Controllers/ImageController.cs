using backend_api.DTO;
using backend_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace backend_api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        //private readonly ILogger<ImageController> _logger;
        //private readonly IDistributedCache _cache;
        //private readonly ImageService _imageService;

        //public ImageController(ILogger<ImageController> logger, IDistributedCache cache, ImageService imageService)
        //{
        //    _logger = logger;
        //    _cache = cache;
        //    _imageService = imageService;
        //}

        //[HttpGet(Name = "GetCounter")]
        //public string get()
        //{
        //    string key = "Counter";
        //    string? result = null;
        //    try
        //    {
        //        var counterStr = _cache.GetString(key);
        //        if (int.TryParse(counterStr, out int counter))
        //        {
        //            counter++;
        //        }
        //        else
        //        {
        //            counter = 0;
        //        }
        //        result = counter.ToString();
        //        _cache.SetString(key, result);
        //    }
        //    catch (RedisConnectionException)
        //    {
        //        result = "Redis cache is not found.";
        //    }
        //    return result;
        //}

        //[HttpGet]
        //public IActionResult getAllImages()
        //{
        //    var dto = _imageService.GetAllImages();
        //    return Ok(dto);
        //}

        //[HttpPut("{id}")]
        //public IActionResult updateImage(Guid id, ImageRequestDTO imageRequestDTO)
        //{
        //    _imageService.createImage(imageRequestDTO);
        //    return Ok();
        //}

        //[HttpPost]
        //public IActionResult createImage(ImageRequestDTO imageRequestDTO)
        //{
        //    _imageService.updateImage(imageRequestDTO);
        //    return Ok("uuid");
        //}
    }
}
