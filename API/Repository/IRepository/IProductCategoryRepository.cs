using API.Data.Models;
using System.Collections.Generic;

namespace API.Repository.IRepository
{
    public interface IProductCategoryRepository
    {
        ICollection<ProductCategory> GetCategories();
        ProductCategory GetCategory(int CategoryId);
        bool CategoryExists(int CategoryId);
        bool CategoryExists(string name);
        bool CreateCategory(ProductCategory productCategory);
        bool UpdateCategory(ProductCategory productCategory);
        bool DeleteCategory(ProductCategory productCategory);
        bool Save();
    }
}
