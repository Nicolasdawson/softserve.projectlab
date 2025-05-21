using API.implementations.Infrastructure.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class ShoppingCartService
{
    private readonly AppDbContext _context;

    public ShoppingCartService(AppDbContext context)
    {
        _context = context;
    }

    //  Agregar producto al carrito
    public async Task<ShoppingCart> AddToCartAsync(int customerId, Guid productId, int quantity)
    {
        var existingItem = await _context.ShoppingCarts
            .FirstOrDefaultAsync(c => c.IdCustomer == customerId && c.IdProduct == productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            existingItem.UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            existingItem = new ShoppingCart
            {
                Id = Guid.NewGuid(),
                IdCustomer = customerId,
                IdProduct = productId,
                Quantity = quantity,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.ShoppingCarts.Add(existingItem);
        }

        await _context.SaveChangesAsync();
        return existingItem;
    }

    //  Obtener el carrito completo de un cliente
    public async Task<List<ShoppingCart>> GetCartByCustomerAsync(int customerId)
    {
        return await _context.ShoppingCarts
            .Where(c => c.IdCustomer == customerId)
            .Include(c => c.Product) // para devolver info del producto
            .ToListAsync();
    }

    // Actualizar cantidad de un ítem
    public async Task UpdateQuantityAsync(int customerId, Guid productId, int newQuantity)
    {
        var cartItem = await _context.ShoppingCarts
            .FirstOrDefaultAsync(c => c.IdCustomer == customerId && c.IdProduct == productId);

        if (cartItem == null)
            throw new Exception("Cart item not found");

        if (newQuantity <= 0)
        {
            _context.ShoppingCarts.Remove(cartItem);
        }
        else
        {
            cartItem.Quantity = newQuantity;
            cartItem.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
    }


    //  Eliminar ítem del carrito
    public async Task RemoveFromCartAsync(Guid cartItemId)
    {
        var item = await _context.ShoppingCarts.FindAsync(cartItemId);
        if (item != null)
        {
            _context.ShoppingCarts.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    //  Vaciar carrito de un cliente (útil al crear orden)
    public async Task ClearCartAsync(int customerId)
    {
        var items = await _context.ShoppingCarts
            .Where(c => c.IdCustomer == customerId)
            .ToListAsync();

        _context.ShoppingCarts.RemoveRange(items);
        await _context.SaveChangesAsync();
    }
}
