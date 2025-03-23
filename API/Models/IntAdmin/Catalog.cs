using API.Models.IntAdmin.Interfaces;

namespace API.Models.IntAdmin
{
    public class Catalog : ICatalog
    {
        public int CatalogId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public List<ICategory> Categories { get; set; } = new List<ICategory>();

        public Catalog(int catalogId, string name, string description, string status, List<ICategory> categories)
        {
            CatalogId = catalogId;
            Name = name;
            Description = description;
            Status = status;
            Categories = categories;
        }

        // Constructor por defecto para escenarios de serialización u otras necesidades.
        public Catalog() { }

        public Result<ICatalog> AddCatalog(ICatalog catalog)
        {
            // Lógica para agregar un nuevo catálogo (ej. guardar en base de datos o colección)
            return Result<ICatalog>.Success(catalog);
        }

        public Result<ICatalog> UpdateCatalog(ICatalog catalog)
        {
            // Lógica para actualizar un catálogo existente
            return Result<ICatalog>.Success(catalog);
        }

        public Result<ICatalog> GetCatalogById(int catalogId)
        {
            // Lógica para obtener un catálogo por su ID
            var catalog = new Catalog(catalogId, "Catálogo de Ejemplo", "Descripción de catálogo de ejemplo", "Activo", new List<ICategory>());
            return Result<ICatalog>.Success(catalog);
        }

        public Result<List<ICatalog>> GetAllCatalogs()
        {
            // Lógica para obtener todos los catálogos
            var catalogs = new List<ICatalog>
            {
                new Catalog(1, "Catálogo 1", "Descripción del catálogo 1", "Activo", new List<ICategory>()),
                new Catalog(2, "Catálogo 2", "Descripción del catálogo 2", "Inactivo", new List<ICategory>())
            };
            return Result<List<ICatalog>>.Success(catalogs);
        }

        public Result<bool> RemoveCatalog(int catalogId)
        {
            // Lógica para eliminar un catálogo por su ID
            return Result<bool>.Success(true);
        }
    }
}
