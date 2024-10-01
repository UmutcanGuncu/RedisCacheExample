using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DistributedCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        IDistributedCache _distributedCache;

        public ValueController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        [HttpGet]
        public async Task<IActionResult> Set(string name, string surname)
        {
            await _distributedCache.SetStringAsync("name", name, options : new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(60),
                SlidingExpiration = TimeSpan.FromSeconds(8)
            });
            await _distributedCache.SetAsync("surname", Encoding.UTF8.GetBytes(surname), options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(60),
                SlidingExpiration = TimeSpan.FromSeconds(8)
            });
            return Ok();
        }
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var name = await _distributedCache.GetStringAsync("name");
            var surnamenameBinary = await _distributedCache.GetAsync("surname");
            var surname = Encoding.UTF8.GetString(surnamenameBinary);
            return Ok(new
            {
                name,
                surname
            });
        }
    }
}

