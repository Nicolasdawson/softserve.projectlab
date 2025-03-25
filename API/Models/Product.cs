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
        public Guid CategoryId { get; set; }
        public string Description { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }


    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}

