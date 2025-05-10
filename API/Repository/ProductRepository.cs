using API.Data;
using API.Data.Models;
using API.Data.Models.DTOs.Product;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.mapper = mapper;
            this.db = db;
        }
        public async Task<bool> AddProduct(Product product)
        {
            try
            {
                await db.Product.AddAsync(product);
                await db.SaveChangesAsync();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var existProduct = await db.Product.FirstOrDefaultAsync(c => c.Id == id);

                if (existProduct != null)
                {
                    db.Product.Remove(existProduct);
                    await db.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch
            {

                return false;
            }
        }

        public Product GetProduct(int productId)
        {
            return db.Product.FirstOrDefault(c => c.Id == productId);
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            IQueryable<Product> query =  db.Product.Include(p => p.Category); 
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.ToLower().Trim().Contains(name.ToLower().Trim()) || c.Detail.Contains(name.ToLower().Trim()));
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<ProductGetDto>> GetProducts(string? productType = null, int? categoryId = null)
        {
            var productsQuery = db.Product.Include(p => p.Category).AsQueryable();
            if (productType == "category" && categoryId != null)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);
            }
            else if (productType == "bestselling")
            {
                productsQuery = productsQuery.Where(p => p.IsBestSelling == true);
            }
            else if (productType == "trending")
            {
                productsQuery = productsQuery.Where(p => p.IsTrending == true);
            }
            else if (productType == null && categoryId == null)
            {
                //trae todos los productos porque no  se aplica ningun filtro
            }
            else
            {
                throw new ArgumentException("Invalid product type");
            }

            var products = await productsQuery.ToListAsync();

            var productGetDto = mapper.Map<IEnumerable<ProductGetDto>>(products);

            return productGetDto;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var productExistente = await db.Product.FindAsync(product.Id);

            try
            {
                if (productExistente != null)
                {
                    db.Entry(productExistente).CurrentValues.SetValues(product);
                    await db.SaveChangesAsync();
                    return true;
                }

                else
                {
                    return false;
                }
            }
            catch 
            {

                return false;
            }
        }
    }
}
