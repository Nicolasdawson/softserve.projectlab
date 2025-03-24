using System.Collections.Generic;

namespace API.Models.IntAdmin.Interfaces
{
    /// <summary>
    /// Represents a category with methods for CRUD operations.
    /// </summary>
    public interface ICategory
    {
        int CategoryId { get; set; }
        string CategoryName { get; set; }
        bool CategoryStatus { get; set; }
        List<Item> Items { get; set; }

        // CRUD methods
        Result<ICategory> AddCategory(ICategory category);
        Result<ICategory> UpdateCategory(ICategory category);
        Result<ICategory> GetCategoryById(int categoryId);
        Result<List<ICategory>> GetAllCategories();
        Result<bool> RemoveCategory(int categoryId);

        // Methods from the diagram
        Result<bool> AddItemToCategory();
        Result<bool> RemoveItemFromCategory();
    }
}
