using API.Entity;
// Microsoft.AspNetCore.Http;
using API.Data.Models;
using Microsoft.AspNetCore.Mvc;
using API.Data;

namespace API.implementations.Domain
{
    public class ProductProcessor : Controller , IProductProcessor
    {
        private readonly ProjectlabContext _db;
        public ProductProcessor(ProjectlabContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public List<Product> GetAllProducts(bool? isActive)
        {
            List<Product> products = new List<Product>();
            products = _db.Products.ToList();
            return products;
        }
        public Product? GetProductByID(int id)
        {
            return new Product();
        }
        public bool AddProduct(string type, Product obj)
        {
            return true;
        }
        public bool UpdateProduct(int id, Product obj)
        {
            return true;
        }
        public bool DeleteProduct(int id)
        {
            return true;
        }

        public bool AddAttribute(int product_id, API.Data.Models.Attribute attribute)
        {
            return true;
        }

        public bool UpdateAttribute(int attribute_id, API.Data.Models.Attribute attribute)
        {
            return true;
        }

        public bool DeleteAttribute(int attribute_id)
        {
            return true;
        }
    }
}
