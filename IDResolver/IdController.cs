using System;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StackExchange.Redis;

namespace IDResolver
{
    public class IdController : Controller
    {
        [HttpPost]
        public IActionResult PostId([FromBody] Element element)
        {
            var redis = ConnectionMultiplexer.Connect("localhost");
            
            var db = redis.GetDatabase();
            db.StringSet(element.Id, element.CallbackUrl);
        
            return Json(element);
        }
    }

    public class Element
    {
        [BindRequired]
        public string Id { set; get; }
        
        [BindRequired]
        public string CallbackUrl { set; get; }
    }
}