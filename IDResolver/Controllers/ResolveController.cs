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
            if (callbackUrl == null || callbackUrl?.Data == "")
                return NotFound("There is no resolve to this ID");
            return Json(callbackUrl);
        }
    }
}