using API.Models.IntAdmin.Interfaces;

namespace API.Models.IntAdmin
{
    public class Category : ICategory
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();

        public Category(int categoryId, string name, string status, List<Item> items)
        {
            CategoryId = categoryId;
            Name = name;
            Status = status;
            Items = items;
        }

        // Constructor por defecto para escenarios de serialización u otras necesidades.
        public Category() { }

        public Result<ICategory> AddCategory(ICategory category)
        {
            // Lógica para agregar una nueva categoría (ej. guardar en base de datos o colección)
            return Result<ICategory>.Success(category);
        }

        public Result<ICategory> UpdateCategory(ICategory category)
        {
            // Lógica para actualizar una categoría existente
            return Result<ICategory>.Success(category);
        }

        public Result<ICategory> GetCategoryById(int categoryId)
        {
            // Lógica para obtener una categoría por su ID
            var category = new Category(categoryId, "Categoría de Ejemplo", "Activo", new List<Item>());
            return Result<ICategory>.Success(category);
        }

        public Result<List<ICategory>> GetAllCategories()
        {
            // Lógica para obtener todas las categorías
            var categories = new List<ICategory>
            {
                new Category(1, "Categoría 1", "Activo", new List<Item>()),
                new Category(2, "Categoría 2", "Inactivo", new List<Item>())
            };
            return Result<List<ICategory>>.Success(categories);
        }

        public Result<bool> RemoveCategory(int categoryId)
        {
            // Lógica para eliminar una categoría por su ID
            return Result<bool>.Success(true);
        }
    }
}
