using API.Controllers;
using API.DTO;
using API.Helpers;
using API.implementations.Infrastructure.Data;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace UnitTests
{
    
    public class ProductTesting
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ProductController _controller;
        private readonly IProductService _productService;
        private readonly IProductImageService _productImageService;
        private readonly IFileStorage _fileStorage;

        public ProductTesting()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=DESKTOP-01G6IST\\SQLEXPRESS;Database=EcommerceDB;User Id=myQueryAccess;Password=ZxcVbn123;TrustServerCertificate=True;")
                .Options;

            _context = new AppDbContext(options);
            _productService = new ProductService(_context);
            _productImageService = new ProductImageService(_context);
            _fileStorage = new FileStorage(_configuration);
            _controller = new ProductController(_configuration, _productService, _productImageService, _fileStorage,_context);
            
        }

        [Fact]
        public async Task Get_Products_paged()
        {
            //Arrange
            var pageNumber = 1;
            var pageSize = 10;

            //Act
            var result = await _controller.GetProducts(pageNumber, pageSize); // getting the ActionResult<T>

            var okResult = Assert.IsType<OkObjectResult>(result.Result); // getting the HTTP response
            var products = Assert.IsAssignableFrom<IEnumerable<ProductWithImagesDTO>>(okResult.Value); // getting the data value

            //Assert            
            Assert.NotNull(products);
            Assert.True(products.Count() <= pageSize, $"Returned {products.Count()} products but expected at most {pageSize}");
            Assert.Equal(pageSize, products.Count());
        }

        [Fact]
        public async Task GetProducts_PageOutOfRange_ReturnEmptyList()
        {
            var pageNumber = 999;
            var pageSize = 10;

            var result = await _controller.GetProducts(pageNumber, pageSize);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var products = Assert.IsAssignableFrom<IEnumerable<ProductWithImagesDTO>>(okResult.Value);

            Assert.Empty(products);
        }

        [Fact]
        public async Task GetProducts_ReturnsValidProducts()
        {
            var result = await _controller.GetProducts(1, 10);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var products = Assert.IsAssignableFrom<IEnumerable<ProductWithImagesDTO>>(okResult.Value);

            Assert.NotEmpty(products);

            foreach (var product in products)
            {
                Assert.False(string.IsNullOrWhiteSpace(product.Name), "Product name should not be null or empty");
                Assert.True(product.Price >= 0, "Product price should be non-negative");
                Assert.True(product.Stock >= 0, "Product stock should be non-negative");
                Assert.NotNull(product.ImageUrls);
            }
        }

        [Theory]
        [InlineData(0, 10)]  // página 0
        [InlineData(-1, 10)] // página negativa
        [InlineData(1, 0)]   // tamaño 0
        [InlineData(1, -5)]  // tamaño negativo
        public async Task GetProducts_InvalidPaginationParameters_ReturnsBadRequest(int pageNumber, int pageSize)
        {
            var result = await _controller.GetProducts(pageNumber, pageSize);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }    
}
