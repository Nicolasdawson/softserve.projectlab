using System.Reflection.Emit;
using API.Helpers;
using API.Models;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.implementations.Infrastructure.Data
{
    public class SeedDb
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IFileStorage _fileStorage;
        
        private readonly StockReservationService _stockService;

        public SeedDb(AppDbContext context, IFileStorage fileStorage, IConfiguration configuration, StockReservationService stockService)
        {
            _context = context;
            _configuration = configuration;
            _fileStorage = fileStorage;
            _stockService = stockService;
        }
        public async Task SeedAsync()
        {
            _context.ClearDatabase(); // Borra los datos sin eliminar la base de datos
            await CheckCategoriesAsync();
            await CheckProductsAsync();
            await CheckLocalImagesAsync();
            //await CheckCountriesAsync();
            //await CheckRegionsAsync();

            //await CheckImagesAsync();
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
                // Adding categories
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
            var cameraCategory = categories.FirstOrDefault(c => c.Name == "Camera");
            var sensorCategory = categories.FirstOrDefault(c => c.Name == "Sensor");
            var alarmCategory = categories.FirstOrDefault(c => c.Name == "Alarm");
            var smartcontrolCategory = categories.FirstOrDefault(c => c.Name == "Smart control");

            // Verifica si ya existen Productos para evitar duplicados
            if (!_context.Products.Any())
            {
                // Adding products
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
                        IdCategory = cameraCategory.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Doorbell Camera Pro",
                        Description = "The Doorbell Camera Pro features smart detection to distinguish people and packages from passing cars, reducing false alerts. With 1080p HDR video, a wide 180° field of view, and night vision, it captures clear footage day or night. Plus, two-way talk and a built-in deterrent help you stay in control of your doorstep from anywhere.",
                        Price = 90.50m,
                        Weight = 1.2m,
                        Height = 8m,
                        Width = 20m,
                        Length = 25m,
                        Stock = 100,
                        IdCategory = cameraCategory.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Indoor Camera Pro",
                        Description = "The best indoor camera uses artificial intelligence to detect people in a specific area. Easily protect off-limits spots in your home like a safe or gun cabinet—customize a detection zone around the area and get alerted if someone approaches. The Indoor Camera Pro's so smart, you'll only get notifications that matter.",
                        Price = 150.50m,
                        Weight = 1.2m,
                        Height = 8m,
                        Width = 20m,
                        Length = 25m,
                        Stock = 100,
                        IdCategory = cameraCategory.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sporlight Pro - Spotlight Camera",
                        Description = "With Smart Deter, the deterrence tech of the Spotlight Pro and Outdoor Camera Pro (Gen 2) keeps unwanted visitors away. When Smart Deter is inactive, the Spotlight Pro keeps you safe by elegantly lighting your walk up the driveway or around the back of the house at night.",
                        Price = 150.50m,
                        Weight = 1.2m,
                        Height = 8m,
                        Width = 20m,
                        Length = 25m,
                        Stock = 100,
                        IdCategory = cameraCategory.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Playback DVR",
                        Description = "Save every minute of video from your indoor, outdoor, and doorbell cameras (Gen 2) for up to 10 days. Thanks to Playback DVR, security camera footage from up to 12 devices is stored directly on the cameras, ensuring a smoother, faster viewing experience on the app with minimal buffering. And because Vivint cameras feature enhanced night vision and audio, you'll always get a clear picture of what's happening in and around your home, day and night.",
                        Price = 150.50m,
                        Weight = 1.2m,
                        Height = 8m,
                        Width = 20m,
                        Length = 25m,
                        Stock = 100,
                        IdCategory = cameraCategory.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Door and Window Sensors",
                        Description = "Prevent disasters, protect what matters. Vivint Door and Window Sensors can also be used on cabinets and safes, so you can minimize the risk of children accessing harmful substances, and maximize the protection of valuables in your home.",
                        Price = 90.50m,
                        Weight = 1.2m,
                        Height = 8m,
                        Width = 20m,
                        Length = 25m,
                        Stock = 100,
                        IdCategory = sensorCategory.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Motion Sensor",
                        Description = "Vivint motion sensors provide complete coverage around the clock. When your system's armed, your motion sensors will trigger a motion alarm, alert our 24/7 monitoring team, and notify you immediately. Vivint motion sensors can communicate directly with your Smart Thermostat, Smart Lighting, and security cameras to adjust temperature, turn off lights, or activate recording. Customize your coverage on the Smart Hub or Vivint app.",
                        Price = 120.99m,
                        Weight = 0.5m,
                        Height = 10m,
                        Width = 15m,
                        Length = 20m,
                        Stock = 50,
                        IdCategory = sensorCategory.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Glass Break Sensor",
                        Description = "With the Vivint Glass Break Sensor, your smart home will hear glass breaking before you do. Get a wider range of coverage, more accurate alarms, and 24/7 monitoring for ultimate protection.",
                        Price = 100.99m,
                        Weight = 2.6m,
                        Height = 3.9m,
                        Width = 15m,
                        Length = 20m,
                        Stock = 50,
                        IdCategory = sensorCategory.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Combination Smoke and Carbon Monoxide Detector",
                        Description = "Get peace of mind and double the safety with a smoke and CO detector that protects against both fire and carbon monoxide—backed by a monitoring team that's always ready to act in an emergency, day or night.",
                        Price = 100.99m,
                        Weight = 2.6m,
                        Height = 3.9m,
                        Width = 15m,
                        Length = 20m,
                        Stock = 50,
                        IdCategory = alarmCategory.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Water Sensor",
                        Description = "The Vivint Water Sensor guards against flood damage with early leak identification, immediate notifications, and 24/7 Monitoring. With battery power and wireless connectivity, this compact smart device is easy to place in locations prone to flooding like basements or near appliances connected to water lines. Installing water sensors may also save you money on home insurance premiums. ",
                        Price = 100.99m,
                        Weight = 2.6m,
                        Height = 3.9m,
                        Width = 15m,
                        Length = 20m,
                        Stock = 50,
                        IdCategory = alarmCategory.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Smart Hub",
                        Description = "The Vivint Smart Hub is what makes everything work together. This intuitive smart panel seamlessly connects your smart home products and bundles your controls on a single platform. Just one tap on the Smart Hub lets you lock your doors, turn off the lights, adjust the thermostat, and arm your system. If you're leaving, it'll count down so you have time to walk out the door. Vivint home automation makes it easy to keep home secure.",
                        Price = 100.99m,
                        Weight = 5,
                        Height = 5.7m,
                        Width = 8.1m,
                        Length = 20m,
                        Stock = 50,
                        IdCategory = smartcontrolCategory.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Vivint Keypad",
                        Description = "Install Vivint Keypad beside your door and easily arm your system and set the status to Away as you leave home—a three-second countdown indicates when your system will arm. Disarm it as you walk in with the press of a button—no need to access the Vivint app or Smart Hub.",
                        Price = 100.99m,
                        Weight = 5,
                        Height = 5.7m,
                        Width = 8.1m,
                        Length = 20m,
                        Stock = 50,
                        IdCategory = smartcontrolCategory.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Vivint Smart Thermostat",
                        Description = "Keep your home temperature exactly where you want it. As part of a Vivint system, the Vivint Smart Thermostat works with your in-home sensors to auto-adjust based on your preferences—whether you're home or away. With the Vivint Smart Thermostat you can start the air conditioning on your way home or lower the heat when you get to work—all from the same app that controls your locks, cameras, and garage door. This wifi thermostat completes your system, ensuring your home temperature stays where you want it, always.",
                        Price = 100.99m,
                        Weight = 5,
                        Height = 5.7m,
                        Width = 8.1m,
                        Length = 20m,
                        Stock = 50,
                        IdCategory = smartcontrolCategory.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Vivint Smart Home Lighting",
                        Description = "Easily integrate the convenience and benefits of Vivint Smart Lighting into your home—no electrical work required. We'll help you create a Smart Lighting Solutions that offers multiple options for lighting control, customization, and automation. All while extending the safety features of your Vivint smart home. No electricians or wiring required. Simply install Smart Light Switches over existing traditional light switches or on any wall in your home for convenient, manual control of your Smart Lighting. The system is so smart you can adjust lighting using the Vivint app or a smart assistant while still controlling lights with the Smart Light Switch.",
                        Price = 100.99m,
                        Weight = 5,
                        Height = 5.7m,
                        Width = 8.1m,
                        Length = 20m,
                        Stock = 50,
                        IdCategory = smartcontrolCategory.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Smart Door Locks",
                        Description = "Smart Locks allow you to assign up to 50 unique access codes for family and friends so they can easily come and go—no more leaving a spare key out in the open. Your smart security system even tracks codes to keep you informed about who locks and unlocks doors at any given time. Using the Vivint app, you can answer your door with the Vivint Doorbell Camera Pro, then control your Smart Locks or Garage Door Controller to let visitors and delivery people inside your home or garage. With instant alerts you'll always know when someone arrives, so you can easily lock up when they leave.",
                        Price = 110.99m,
                        Weight = 5,
                        Height = 5.7m,
                        Width = 8.1m,
                        Length = 20m,
                        Stock = 50,
                        IdCategory = smartcontrolCategory.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                );
            }
            _context.SaveChanges(); // Saving products
             var products = await _context.Products.ToListAsync();
            foreach (var product in products)
            {
                await _stockService.SetStockAsync(product.Id, product.Stock);
            }
        }

        private async Task CheckLocalImagesAsync()
        {
            var products = await _context.Products.ToListAsync();
            var glassBreakProduct = products.FirstOrDefault(p => p.Name == "Glass Break Sensor");
            var smartHomeProduct = products.FirstOrDefault(p => p.Name == "Vivint Smart Home Lighting");
            var doorbellCameraProduct = products.FirstOrDefault(p => p.Name == "Doorbell Camera Pro");
            var smartDoorLocksProduct = products.FirstOrDefault(p => p.Name == "Smart Door Locks");
            var sportlightProProduct = products.FirstOrDefault(p => p.Name == "Sporlight Pro - Spotlight Camera");
            var combinSmokeandCarbonProduct = products.FirstOrDefault(p => p.Name == "Combination Smoke and Carbon Monoxide Detector");
            var outdoorCameraProduct = products.FirstOrDefault(p => p.Name == "Outdoor Camera Pro");
            var indoorCameraProduct = products.FirstOrDefault(p => p.Name == "Indoor Camera Pro");
            var doorAndWindowProduct = products.FirstOrDefault(p => p.Name == "Door and Window Sensors");
            var keypadProduct = products.FirstOrDefault(p => p.Name == "Vivint Keypad");
            var motionSensorProduct = products.FirstOrDefault(p => p.Name == "Motion Sensor");
            var smartHubProduct = products.FirstOrDefault(p => p.Name == "Smart Hub");
            var playbackDvrProduct = products.FirstOrDefault(p => p.Name == "Playback DVR");
            var smartThermoProduct = products.FirstOrDefault(p => p.Name == "Vivint Smart Thermostat");
            var waterSensorProduct = products.FirstOrDefault(p => p.Name == "Water Sensor");

            var filePath = $"{Environment.CurrentDirectory}\\Images\\Products\\";
            var filesNames = Directory
                      .GetFiles(filePath)
                      .Select(Path.GetFileName)
                      .ToArray();

 
            
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Products");

            var images = new List<ProductImage>();
            foreach (var item in products)
            {
                // Contar cuántas imágenes tiene el producto
                int currentImagesCount = await _context.ProductImages.CountAsync(pi => pi.IdProduct == item.Id);

                // Si ya tiene 3 o más imágenes, saltar este producto
                if (currentImagesCount >= 3)
                {
                    continue; // ya tiene imágenes, no agregamos más
                }

                int index = Array.FindIndex(filesNames, name => name.StartsWith(item.Name, StringComparison.OrdinalIgnoreCase));
                if (index == -1) break;

                for (int i = 0; i < 3; i++)
                {
                                                
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(item.Name);                    
                    images.Add(new ProductImage
                    {
                        Id = Guid.NewGuid(),
                        ImageUrl = _configuration["urlBackEnd"] + "/Images/" + filesNames[index + i],
                        IdProduct = item.Id,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,

                    });
                }
            }

            ProductImage[] imageArr = images.ToArray();
            
            await _context.ProductImages.AddRangeAsync(imageArr);
            await _context.SaveChangesAsync();                
            
        }

        private async Task CheckImagesAsync()
        {            
            var products = await _context.Products.ToListAsync();
            var glassBreakProduct = products.FirstOrDefault(p => p.Name == "Glass Break Sensor");
            var smartHomeProduct = products.FirstOrDefault(p => p.Name == "Vivint Smart Home Lighting");
            var doorbellCameraProduct = products.FirstOrDefault(p => p.Name == "Doorbell Camera Pro");
            var smartDoorLocksProduct = products.FirstOrDefault(p => p.Name == "Smart Door Locks");
            var sportlightProProduct = products.FirstOrDefault(p => p.Name == "Sporlight Pro - Spotlight Camera");
            var combinSmokeandCarbonProduct = products.FirstOrDefault(p => p.Name == "Combination Smoke and Carbon Monoxide Detector");
            var outdoorCameraProduct = products.FirstOrDefault(p => p.Name == "Outdoor Camera Pro");
            var indoorCameraProduct = products.FirstOrDefault(p => p.Name == "Indoor Camera Pro");
            var doorAndWindowProduct = products.FirstOrDefault(p => p.Name == "Door and Window Sensors");
            var keypadProduct = products.FirstOrDefault(p => p.Name == "Vivint Keypad");
            var motionSensorProduct = products.FirstOrDefault(p => p.Name == "Motion Sensor");
            var smartHubProduct = products.FirstOrDefault(p => p.Name == "Smart Hub");
            var playbackDvrProduct = products.FirstOrDefault(p => p.Name == "Playback DVR");
            var smartThermoProduct = products.FirstOrDefault(p => p.Name == "Vivint Smart Thermostat");
            var waterSensorProduct = products.FirstOrDefault(p => p.Name == "Water Sensor");

            var filePath = $"{Environment.CurrentDirectory}\\Images\\Products\\";
            var filesNames = Directory
                      .GetFiles(filePath)
                      .Select(Path.GetFileName)
                      .ToArray();

            var images = new List<ProductImage>();
            foreach ( var item in products)
            {
                if (_context.ProductImages.Any(img => img.IdProduct == item.Id))
                continue;


                int index = Array.FindIndex(filesNames, name => name.StartsWith(item.Name, StringComparison.OrdinalIgnoreCase));

                for (int i = 0; i < 3; i++) {
                    var fileBytes = File.ReadAllBytes(filePath + filesNames[index + i]);
                    var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "PNG", "products");
                    Console.WriteLine("Agregando nuevo objeto a la lista images");
                    images.Add(new ProductImage
                    {
                        Id = Guid.NewGuid(),
                        ImageUrl = imagePath,
                        IdProduct = item.Id,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,

                    });
                }
            }

            ProductImage[] imageArr = images.ToArray();

            Console.WriteLine("Ejecutando SaveChangesAsync...");
            await _context.ProductImages.AddRangeAsync(imageArr);
            await _context.SaveChangesAsync(); // Guardar las imagenes                    
            Console.WriteLine("Imágenes guardadas.");


        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                var countriesSqlScript = File.ReadAllText("implementations\\Infrastructure\\Data\\Countries.sql");
                await _context.Database.ExecuteSqlRawAsync(countriesSqlScript);
            }
        }

        private async Task CheckRegionsAsync()
        {
            if (!_context.Regions.Any())
            {
                var countriesSqlScript = File.ReadAllText("implementations\\Infrastructure\\Data\\Regions.sql");
                await _context.Database.ExecuteSqlRawAsync(countriesSqlScript);
            }
        }
    }
}