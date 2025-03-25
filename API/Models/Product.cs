using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;  // Necesario para usar Data Annotations
using Newtonsoft.Json;

namespace API.Models;

    public class Product
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public string Name { get; set; } = default!;

        [JsonProperty("product_categories")]  
        public List<string> Category { get; set; } = new();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]  
        public string? Description { get; set; } 

        [JsonProperty("image")]  
        public string? ImageFile { get; set; } 

        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un número positivo")]
        public decimal Price { get; set; }
    }

