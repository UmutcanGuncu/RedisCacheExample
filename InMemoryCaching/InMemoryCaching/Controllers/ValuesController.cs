using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InMemoryCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        /*
        [HttpGet("set/{name}")]
        public void SetName(string name)
        {
            _memoryCache.Set("Name", name);
        }
        [HttpGet]
        public string GetName()
        {
            if (_memoryCache.TryGetValue<string>("Name",out string name))
            {
                return name;
            }
            return "";
            
           //_memoryCache.Get("Name"); Name keyine karşılık gelen value object olarak gelecektir
            //return  _memoryCache.Get<string>("Name"); Name keyine karşılık gelen value string olrk gelecektir
        }
        */
        [HttpGet("setDate")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(60), // veri 60 saniye sonra cache'den silinecek
                SlidingExpiration = TimeSpan.FromSeconds(10) //10 saniye boyunca veriyle işlem yapmazsakta silinecek
            });

        }
        [HttpGet("getDate")]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }
    }
}

