using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.implementations.Infrastructure.Data;

namespace API.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public IEnumerable<API.Models.Category> Categories { get; set; }


        public IndexModel(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));  // Validación de contexto
            Categories = new List<API.Models.Category>();
        }

        public IList<API.Models.Category> Category { get;set; } = default!;
        public async Task OnGetAsync()
        {
            Categories = await _context.Categories.ToListAsync();  // Esto obtiene las categorías
            if (Categories == null)
            {
                Categories = new List<API.Models.Category>();  // En caso de que Categories sea null (aunque no debería serlo)
            }
        }
    }
}
