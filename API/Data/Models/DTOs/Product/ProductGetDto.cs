using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace API.Data.Models.DTOs.Product
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
    }
}
