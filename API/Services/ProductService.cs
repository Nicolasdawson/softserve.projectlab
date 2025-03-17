using API.Models;

namespace API.Services;

public class ProductService
{
    private readonly List<Product> _products = new List<Product>();

    public Result<Product> CreateProduct(Product product)
    {
        try
        {
            product.Id = Guid.NewGuid(); // Generar un nuevo ID
            _products.Add(product); // Agregar el producto a la lista en memoria
            Console.WriteLine($"Producto creado: {product.Name} con ID {product.Id}");

            return Result<Product>.Success(product); // Devolver el producto creado con éxito
        }
        catch (Exception ex)
        {
            return Result<Product>.Failure($"Error al crear el producto: {ex.Message}"); // Si ocurre un error, devolverlo en el resultado
        }
    }

    public Result<IEnumerable<Product>> GetAllProducts()
    {
        try
        {
            Console.WriteLine($"Cantidad de productos en memoria: {_products.Count}"); // Log para verificar la cantidad de productos
            return Result<IEnumerable<Product>>.Success(_products);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Product>>.Failure($"Error al obtener productos: {ex.Message}");
        }
    }

    public Result<PagedResult<Product>> GetProductsFiltered(
        string? name = null, 
        List<string>? category = null, 
        decimal? minPrice = null, 
        decimal? maxPrice = null, 
        int pageNumber = 1, 
        int pageSize = 10)
    {
        try
        {
            var query = _products.AsQueryable();

            // Aplicar filtros por nombre, categoría, precio mínimo y máximo
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (category != null && category.Any())
            {
                query = query.Where(p => p.Category.Intersect(category).Any());
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            // Obtener el total de productos antes de la paginación
            var totalCount = query.Count();

            // Paginación
            var skip = (pageNumber - 1) * pageSize;
            var paginatedProducts = query.Skip(skip).Take(pageSize).ToList();

            var pagedResult = new PagedResult<Product>
            {
                Items = paginatedProducts,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Result<PagedResult<Product>>.Success(pagedResult);
        }
        catch (Exception ex)
        {
            return Result<PagedResult<Product>>.Failure($"Error al obtener productos filtrados: {ex.Message}");
        }
    }

    public Result<Product> GetProductById(Guid id)
    {
        try
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return Result<Product>.Failure("Producto no encontrado.");
            }

            return Result<Product>.Success(product);
        }
        catch (Exception ex)
        {
            return Result<Product>.Failure($"Error al obtener el producto: {ex.Message}");
        }
    }

    public Result<bool> DeleteProduct(Guid id)
    {
        try
        {
            var product = GetProductById(id).Data;
            if (product == null)
            {
                return Result<bool>.Failure("Producto no encontrado.");
            }

            _products.Remove(product);
            return Result<bool>.Success(true); // Producto eliminado correctamente
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Error al eliminar el producto: {ex.Message}");
        }
    }

    public Result<bool> UpdateProduct(Guid id, Product updatedProduct)
    {
        try
        {
            var existingProduct = GetProductById(id).Data;
            if (existingProduct == null)
            {
                return Result<bool>.Failure("Producto no encontrado.");
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Category = updatedProduct.Category;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.ImageFile = updatedProduct.ImageFile;
            existingProduct.Price = updatedProduct.Price;

            return Result<bool>.Success(true); // Actualización exitosa
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Error al actualizar el producto: {ex.Message}");
        }
    }
}
