using Microsoft.AspNetCore.Mvc;

namespace IDResolver
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}