using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Logistics
{
    public class SupplierController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
