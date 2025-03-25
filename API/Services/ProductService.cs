// Services/ProductService.cs
using API.implementations.Infrastructure.Data;
using API.Models;

namespace API.Services
{
    public class ProductService
    {
        private readonly AppDbContext _context; 
        private readonly List<Product> _products;

        public ProductService(AppDbContext context) 
        {
            _context = context;
            /*
            _products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "C�mara de Seguridad IP 1080p",
                    Category = "C�maras de Seguridad",
                    Description = "C�mara de seguridad de alta definici�n con visi�n nocturna y grabaci�n en 1080p. Conectividad Wi-Fi y detecci�n de movimiento.",
                    ImageUrl = "https://example.com/images/camara-ip-1080p.jpg",
                    Price = 120.99m,
                    Stock = "Disponible"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Alarma Inal�mbrica 4 Zonas",
                    Category = "Alarmas",
                    Description = "Sistema de alarma inal�mbrico con 4 zonas, ideal para viviendas. Compatible con sensores de puertas y ventanas.",
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
                    Name = "C�mara de Seguridad Dome 4K",
                    Category = "C�maras de Seguridad",
                    Description = "C�mara dome 4K con visi�n panor�mica y grabaci�n en calidad ultra HD. Resistente a condiciones clim�ticas extremas.",
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
                    Name = "C�mara de Seguridad para Exteriores",
                    Category = "C�maras de Seguridad",
                    Description = "C�mara de seguridad para exteriores, resistente al agua y con visi�n nocturna. Se conecta a trav�s de Wi-Fi.",
                    ImageUrl = "https://example.com/images/camara-para-exteriores.jpg",
                    Price = 180.75m,
                    Stock = "Disponible"
                }
            };
             */
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
            //Console.WriteLine($"Cantidad de productos en memoria: {_products.Count}"); // Log para verificar la cantidad de productos
            return _context.Products.ToList();
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
                existingProduct.CategoryId = updatedProduct.CategoryId;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.ImageUrl = updatedProduct.ImageUrl;
                existingProduct.Price = updatedProduct.Price;
                return true;
            }
            return false;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _products;//.Where(p => p.CategoryId.Equals(category, StringComparison.OrdinalIgnoreCase));
        }
    }
}