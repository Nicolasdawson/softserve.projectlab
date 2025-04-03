using API.Data.Models;
using API.Data;
using API.Repository.IRepository;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbAb6d2eProjectlabContext _dbContext;

        public ProductRepository(DbAb6d2eProjectlabContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CreateProduct(Product product)
        {
            _dbContext.Products.Add(product);
            return Save();
        }

        public bool DeleteProduct(Product product)
        {
            _dbContext.Products.Remove(product);
            return Save();
        }

        public Product GetProduct(int productId)
        {
            return _dbContext.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == productId);
        }

        public ICollection<Product> GetProducts()
        {
            return _dbContext.Products.Include(p => p.Category).OrderBy(c => c.Id).ToList();
        }

        public bool ProductExists(int productId)
        {
            return _dbContext.Products.Any(c => c.Id == productId);
        }

        public bool ProductExists(string name)
        {
            return _dbContext.Products.Any(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0; ;
        }

        public bool UpdateProduct(Product product)
        {
            _dbContext.Products.Update(product);
            return Save();
        }
    }
}
