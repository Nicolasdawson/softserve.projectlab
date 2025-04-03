using API.Data.Models;
using API.Data;
using API.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly DbAb6d2eProjectlabContext _dbContext;

        public ProductCategoryRepository(DbAb6d2eProjectlabContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CategoryExists(int CategoryId)
        {
            return _dbContext.ProductCategories.Any(c => c.Id == CategoryId);
        }

        public bool CategoryExists(string name)
        {
            return _dbContext.ProductCategories.Any(c => c.Category.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool CreateCategory(ProductCategory productCategory)
        {
            _dbContext.ProductCategories.Add(productCategory);
            return Save();
        }

        public bool DeleteCategory(ProductCategory productCategory)
        {
            _dbContext.ProductCategories.Remove(productCategory);
            return Save();
        }

        public ICollection<ProductCategory> GetCategories()
        {
            return _dbContext.ProductCategories.OrderBy(c => c.Id).ToList();
        }

        public ProductCategory GetCategory(int CategoryId)
        {
            return _dbContext.ProductCategories.FirstOrDefault(c => c.Id == CategoryId);
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool UpdateCategory(ProductCategory productCategory)
        {
            _dbContext.ProductCategories.Update(productCategory);
            return Save();
        }
    }
}
