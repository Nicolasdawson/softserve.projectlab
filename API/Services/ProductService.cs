// Services/ProductService.cs
using API.Models;

namespace API.Services
{
    public class ProductService
    {
        private readonly List<Product> _products;

        public ProductService() 
        {
            _products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Cámara de Seguridad IP 1080p",
                    Category = "Cámaras de Seguridad",
                    Description = "Cámara de seguridad de alta definición con visión nocturna y grabación en 1080p. Conectividad Wi-Fi y detección de movimiento.",
                    ImageUrl = "https://example.com/images/camara-ip-1080p.jpg",
                    Price = 120.99m,
                    Stock = "Disponible"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Alarma Inalámbrica 4 Zonas",
                    Category = "Alarmas",
                    Description = "Sistema de alarma inalámbrico con 4 zonas, ideal para viviendas. Compatible con sensores de puertas y ventanas.",
                    ImageUrl = "https://example.com/images/alarma-inalambrica.jpg",
                    Price = 150.50m,
                    Stock = "Disponible"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Sensor de Movimiento PIR",
                    Category = "Sensores",
                    Description = "Sensor de movimiento PIR (infrarrojo pasivo) para sistemas de alarma. Detecta movimiento en un rango de hasta 10 metros.",
                    ImageUrl = "https://example.com/images/sensor-movimiento-pir.jpg",
                    Price = 45.30m,
                    Stock = "Agotado"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Cámara de Seguridad Dome 4K",
                    Category = "Cámaras de Seguridad",
                    Description = "Cámara dome 4K con visión panorámica y grabación en calidad ultra HD. Resistente a condiciones climáticas extremas.",
                    ImageUrl = "https://example.com/images/camara-dome-4k.jpg",
                    Price = 299.99m,
                    Stock = "Disponible"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Alarma para Puerta/ ventana",
                    Category = "Alarmas",
                    Description = "Alarma de seguridad para puertas y ventanas. Ideal para prevenir accesos no autorizados en el hogar o negocio.",
                    ImageUrl = "https://example.com/images/alarma-puerta-ventana.jpg",
                    Price = 32.99m,
                    Stock = "Disponible"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Cámara de Seguridad para Exteriores",
                    Category = "Cámaras de Seguridad",
                    Description = "Cámara de seguridad para exteriores, resistente al agua y con visión nocturna. Se conecta a través de Wi-Fi.",
                    ImageUrl = "https://example.com/images/camara-para-exteriores.jpg",
                    Price = 180.75m,
                    Stock = "Disponible"
                }
            };
        }
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
                existingProduct.ImageUrl = updatedProduct.ImageUrl;
                existingProduct.Price = updatedProduct.Price;
                return true;
            }
            return false;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }
    }
}
