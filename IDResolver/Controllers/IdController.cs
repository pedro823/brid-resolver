using IDResolver.Database;
using IDResolver.Interface;
using IDResolver.Models;
using Microsoft.AspNetCore.Mvc;

namespace IDResolver.Controllers
{
    public class IdController : Controller
    {
        [HttpPost("/id")]
        public IActionResult PostId([FromBody] Element element)
        {
            // not awaited
            if (element.HasNullValues())
            {
                return BadRequest(new BridResolverException("Body must contain \"id\" and \"callbackUrl\"").ToString());
            }
            RedisDatabase.Set(element.Id, element.CallbackUrl);
            return Json(element);
        }
    }
}