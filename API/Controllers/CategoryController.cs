using Microsoft.AspNetCore.Mvc;
using API.implementations.Infrastructure.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using API.Pages.Category;

namespace API.Controllers
{

    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Category
        /*
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();  // Uso de _context.Categories desde AppDbContext
            return View(categories);  // Devuelve la vista Index.cshtml con las categorías
        }
        // GET: Category/Index
        public async Task<IActionResult> Index()
        {
            // Pasar la lista de categorías al modelo de la vista
            var model = new IndexModel
            {
                Categories = await _context.Categories.ToListAsync()
            };
            return View(model);  // Asegúrate de pasar el modelo correcto
        }
         */

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Categories"; // Asegúrate de que esta línea está en la acción.
            var categories = await _context.Categories.ToListAsync();
            foreach (var category in categories)
            {
                Console.WriteLine($"Category Name: {category.Name}");
            }
            return View(categories);
        }

        // GET: /Category/Create
        public IActionResult Create()
        {
            return View();  // Devuelve la vista Create.cshtml
        }

        // POST: /Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));  // Redirige a la vista Index después de guardar
            }
            return View(category);  // Vuelve a mostrar la vista Create.cshtml con el modelo si hay errores
        }

        // GET: /Category/Details/{id}
        public IActionResult Details(Guid id)
        {
            var category = _context.Categories
                .FirstOrDefault(m => m.Id == id);  // Uso de _context.Categories desde AppDbContext
            if (category == null)
            {
                return NotFound();  // Devuelve un error 404 si no se encuentra la categoría
            }
            return View(category);  // Devuelve la vista Details.cshtml con la categoría encontrada
        }

        // GET: /Category/Edit/{id}
        public IActionResult Edit(Guid id)
        {
            var category = _context.Categories.Find(id);  // Uso de _context.Categories desde AppDbContext
            if (category == null)
            {
                return NotFound();  // Devuelve un error 404 si no se encuentra la categoría
            }
            return View(category);  // Devuelve la vista Edit.cshtml con la categoría encontrada
        }

        // POST: /Category/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();  // Si el ID no coincide, devuelve un error 404
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizamos el campo UpdatedAt al momento de la edición
                    category.UpdatedAt = DateTime.UtcNow;
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Categories.Any(e => e.Id == category.Id))  // Uso de _context.Categories desde AppDbContext
                    {
                        return NotFound();  // Devuelve un error si no se encuentra la categoría
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));  // Redirige a Index después de la edición
            }
            return View(category);  // Si el modelo no es válido, vuelve a mostrar la vista Edit.cshtml
        }

        // GET: /Category/Delete/{id}
        public IActionResult Delete(Guid id)
        {
            var category = _context.Categories
                .FirstOrDefault(m => m.Id == id);  // Uso de _context.Categories desde AppDbContext
            if (category == null)
            {
                return NotFound();  // Devuelve un error 404 si no se encuentra la categoría
            }
            return View(category);  // Devuelve la vista Delete.cshtml con la categoría encontrada
        }

        // POST: /Category/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);  // Uso de _context.Categories desde AppDbContext
            if (category != null)
            {
                _context.Categories.Remove(category);  // Remueve la categoría de _context.Categories
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));  // Redirige a Index después de eliminar la categoría
        }
    }
}
