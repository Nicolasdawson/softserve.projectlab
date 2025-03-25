using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.implementations.Infrastructure.Data
{
    public static class SeedDataExtension
    {
        public static void SeedProducts(this ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Product>().HasData( 
                new Product[]
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
            });
             */
        }
    }
}
