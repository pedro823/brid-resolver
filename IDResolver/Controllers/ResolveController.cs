using System.Threading.Tasks;
using IDResolver.Database;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace IDResolver.Controllers
{
    public class ResolveController : Controller
    {
        [HttpGet("/id/{id}")]
        public async Task<IActionResult> ResolveId(string id)
        {
            var callbackUrl = await RedisDatabase.Get<string>(id);
            if (string.IsNullOrEmpty(callbackUrl?.Data))
            {
                return NotFound();
            }
            return Content(callbackUrl.Data);
        }

        [HttpGet("/id/{id}/unique")]
        public async Task<IActionResult> IsUniqueId(string id)
        {
            var callbackUrl = await RedisDatabase.Get<string>(id);
            return Content(JsonConvert.SerializeObject(callbackUrl == null));
        }
    }
}