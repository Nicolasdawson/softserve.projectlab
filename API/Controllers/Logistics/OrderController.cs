using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Logistics
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
