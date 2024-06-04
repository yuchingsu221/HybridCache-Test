using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Hybrid;
using System.Threading.Tasks;

namespace HybridCacheExample.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly HybridCache _hybridCache;

        public CacheController(HybridCache hybridCache)
        {
            _hybridCache = hybridCache;
        }

        [HttpPost("set")]
        public async Task<IActionResult> SetCacheValue([FromQuery] string key, [FromQuery] string value)
        {
            await _hybridCache.SetAsync(key, value);
            return Ok("Value set successfully");
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetCacheValue([FromQuery] string key)
        {
            var value = await _hybridCache.GetOrCreateAsync<string>(key, async _ =>
            {
                await Task.Delay(50); // Simulate some data fetching delay
                return "default value";
            });

            return Ok(value);
        }
    }
}
