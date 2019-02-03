using IDResolver.Database;
using IDResolver.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IDResolver.Controllers
{
    public class IdController : Controller
    {
        [HttpPost("/id")]
        public IActionResult PostId([FromBody] Element element)
        {
            // not awaited
            RedisDatabase.Set(element.Id, element.CallbackUrl);
            return Json(element);
        }
    }
}