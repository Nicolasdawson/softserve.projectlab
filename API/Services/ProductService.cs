// Services/ProductService.cs
using API.Models;

namespace API.Services
{
    public class ProductService
    {
        private readonly List<Product> _products = new List<Product>();

        public Product CreateProduct(Product product)
        {
            product.Id = Guid.NewGuid(); // Generar un nuevo ID
            _products.Add(product); // Agregar el producto a la lista en memoria
            Console.WriteLine($"Producto creado: {product.Name} con ID {product.Id}"); // Log para verificar si se agrega
            return product;
        }


        public IEnumerable<Product> GetAllProducts()
        {
            Console.WriteLine($"Cantidad de productos en memoria: {_products.Count}"); // Log para verificar la cantidad de productos
            return _products;
        }


        public Product? GetProductById(Guid id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public bool DeleteProduct(Guid id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                _products.Remove(product);
                return true;
            }
            return false;
        }

        public bool UpdateProduct(Guid id, Product updatedProduct)
        {
            var existingProduct = GetProductById(id);
            if (existingProduct != null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Category = updatedProduct.Category;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.ImageFile = updatedProduct.ImageFile;
                existingProduct.Price = updatedProduct.Price;
                return true;
            }
            return false;
        }
    }
}
