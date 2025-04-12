using System.Reflection.Emit;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.implementations.Infrastructure.Data
{
    public class SeedDb
    {
        private readonly AppDbContext _context;
        
        public SeedDb(AppDbContext context)
        {
            _context = context;
        }
        public async Task SeedAsync()
        {
            _context.ClearDatabase(); // Borra los datos sin eliminar la base de datos
            await CheckCategoriesAsync();
            //await CheckProductsAsync();

            /*
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = category1,
                    Name = "Cámaras de Seguridad",
                },
                new Category
                {
                    Id = category2,
                    Name = "Alarmas",
                },
                new Category
                {
                    Id = category3,
                    Name = "Sensores",
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product[]
                {
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Cámara de Seguridad IP 1080p",
                        Description = "Cámara de seguridad de alta definición con visión nocturna y grabación en 1080p. Conectividad Wi-Fi y detección de movimiento.",
                        Price = 120.99m,
                        Weight = 0.5m,
                        Height = 10m,
                        Width = 15m,
                        Length = 20m,
                        Stock = 50,
                        IdCategory = category1 // Reutilizando el Id de la categoría "Cámaras de Seguridad"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Alarma Inalámbrica 4 Zonas",
                        Description = "Sistema de alarma inalámbrico con 4 zonas, ideal para viviendas. Compatible con sensores de puertas y ventanas.",
                        Price = 150.50m,
                        Weight = 1.2m,
                        Height = 8m,
                        Width = 20m,
                        Length = 25m,
                        Stock = 100,
                        IdCategory = category2 // Reutilizando el Id de la categoría "Alarmas"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sensor de Movimiento PIR",
                        Description = "Sensor de movimiento PIR (infrarrojo pasivo) para sistemas de alarma. Detecta movimiento en un rango de hasta 10 metros.",
                        Price = 45.30m,
                        Weight = 0.3m,
                        Height = 6m,
                        Width = 8m,
                        Length = 12m,
                        Stock = 0,
                        IdCategory = category3 // Reutilizando el Id de la categoría "Sensores"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Cámara de Seguridad Dome 4K",
                        Description = "Cámara dome 4K con visión panorámica y grabación en calidad ultra HD. Resistente a condiciones climáticas extremas.",
                        Price = 299.99m,
                        Weight = 0.8m,
                        Height = 12m,
                        Width = 15m,
                        Length = 18m,
                        Stock = 50,
                        IdCategory = category1 // Reutilizando el Id de la categoría "Cámaras de Seguridad"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Alarma para Puerta/ ventana",
                        Description = "Alarma de seguridad para puertas y ventanas. Ideal para prevenir accesos no autorizados en el hogar o negocio.",
                        Price = 32.99m,
                        Weight = 0.5m,
                        Height = 5m,
                        Width = 10m,
                        Length = 15m,
                        Stock = 200,
                        IdCategory = category2 // Reutilizando el Id de la categoría "Alarmas"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Cámara de Seguridad para Exteriores",
                        Description = "Cámara de seguridad para exteriores, resistente al agua y con visión nocturna. Se conecta a través de Wi-Fi.",
                        Price = 180.75m,
                        Weight = 1.0m,
                        Height = 10m,
                        Width = 20m,
                        Length = 25m,
                        Stock = 30,
                        IdCategory = category1 // Reutilizando el Id de la categoría "Cámaras de Seguridad"
                    }
                }
            );
             */
        }

        private async Task CheckCategoriesAsync()
        {

            // Verifica si ya existen categorías para evitar duplicados
            if (!_context.Categories.Any())
            {
                // Crear las categorías primero
                // Agregar categorías
                var category1 = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Camera",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                var category2 = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Alarm",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                var category3 = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Sensor",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                var category4 = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Smart control",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                var category5 = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Service",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                var category6 = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Plan",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Categories.AddRange(category1, category2, category3, category4, category5, category6);
                _context.SaveChanges(); // Guardar las categorías
            }
        }

        private async Task CheckProductsAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            foreach (var category in categories)
            {
                Console.WriteLine("----------------------------------------------------------------------------------------");
                Console.WriteLine(category);
            }
            // Verifica si ya existen Productos para evitar duplicados
            /*
             
            if (!_context.Products.Any())
            {
                // Agregar productos
                _context.Products.AddRange(
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Outdoor Camera Pro",
                        Description = "The Outdoor Camera Pro uses smart person detection to distinguish people from pets or passing cars, reducing false alerts. With a 4K HDR sensor, enhanced night vision, and Smart Deter—its built-in deterrent that activates when a threat is detected—the camera delivers crystal-clear footage and proactive protection, day or night.",
                        Price = 120.99m,
                        Weight = 0.5m,
                        Height = 10m,
                        Width = 15m,
                        Length = 20m,
                        Stock = 50,
                        IdCategory = categories[0].Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Alarma Inalámbrica 4 Zonas",
                        Description = "Sistema de alarma inalámbrico con 4 zonas.",
                        Price = 150.50m,
                        Weight = 1.2m,
                        Height = 8m,
                        Width = 20m,
                        Length = 25m,
                        Stock = 100,
                        IdCategory = category2.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );
            }
            dbContext.SaveChanges(); // Guardar los productos
             */

        }

    }
}