using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace DistributedCaching.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        public TestController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [Route("add-cache-no-time-options")]
        [HttpGet]
        public async Task<IActionResult> AddCacheNoTimeOptions()
        {
            string key = "Test1";
            string value = "Equiniti";
            await _distributedCache.SetStringAsync(key, value);
            return Ok("success");
        }

        [Route("add-cache")]
        [HttpGet]
        public async Task<IActionResult> AddCache()
        {
            string key = "Test2";
            string value = "Equiniti ICS";
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(1),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            };
            await _distributedCache.SetStringAsync(key, value, options);
            return Ok("success");
        }

        [Route("get-cache")]
        [HttpGet]
        public async Task<IActionResult> GetCache()
        {
            string name = await _distributedCache.GetStringAsync("Test2");
            return Ok(name);
        }


    }
}
