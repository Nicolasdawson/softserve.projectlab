using API.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.implementations.Domain
{
    public class ProductDomain : Controller
    {
        private readonly AppDbContext _db;
        public ProductDomain(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductDomain/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductDomain/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductDomain/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductDomain/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductDomain/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductDomain/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductDomain/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
