using System.Threading.Tasks;
using IDResolver.Database;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace IDResolver.Controllers
{
    public class ResolveController : Controller
    {
        public async Task<IActionResult> ResolveId(string id)
        {
            var callbackUrl = await RedisDatabase.Get<string>(id);
            return Content(callbackUrl);
        }
    }
}