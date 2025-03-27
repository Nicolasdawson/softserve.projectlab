using API.Entity;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.implementations.Domain
{
    public class ProductProcessor : Controller , IProductProcessor
    {
        private readonly AppDbContext _db;
        public ProductProcessor(AppDbContext db)
        {
            _db = db;
        }
        public List<Product> GetAllProducts(bool? isActive)
        {
            bool isAnyProducts = _db.Products.Any();
            List<Product> products = new List<Product>();
            if (isAnyProducts)
            {
                products = _db.Products.ToList();
            }

            return products;
        }
        public Product? GetProductByID(int id)
        {
            return new Product();
        }
        public Product? AddProduct(Product obj)
        {
            return new Product();
        }
        public Product? UpdateProduct(int id, Product obj)
        {
            return new Product();
        }
        public bool DeleteProduct(int id)
        {
            return true;
        }
    }
}
