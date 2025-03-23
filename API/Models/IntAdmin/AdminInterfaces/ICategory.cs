using API.Models.IntAdmin;

namespace API.Models.IntAdmin.Interfaces
{
    public interface ICategory
    {
        int CategoryId { get; set; }
        string Name { get; set; }
        string Status { get; set; }
        List<Item> Items { get; set; }

        Result<ICategory> AddCategory(ICategory category);
        Result<ICategory> UpdateCategory(ICategory category);
        Result<ICategory> GetCategoryById(int categoryId);
        Result<List<ICategory>> GetAllCategories();
        Result<bool> RemoveCategory(int categoryId);
    }
}
