using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Logistics
{
    public class BranchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
