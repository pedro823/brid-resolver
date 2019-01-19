using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace IDResolver.Controllers
{
    public class ResolveController : Controller
    {
        public async Task<IActionResult> ResolveId(string id)
        {
            var redis = ConnectionMultiplexer.Connect("localhost");
            var db = redis.GetDatabase();
            var callbackUrl = await db.StringGetAsync(id);
            return Content(callbackUrl);
        }
    }
}