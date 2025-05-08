using API.Data;
using API.Data.Entities;
using API.Repositories.LogisticsRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.LogisticsRepositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderEntity?> GetByIdAsync(int orderId)
        {
            return await _context.OrderEntities
                .Include(o => o.OrderItemEntities) // Include related items
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<List<OrderEntity>> GetAllAsync()
        {
            return await _context.OrderEntities
                .Include(o => o.OrderItemEntities) // Include related items
                .ToListAsync();
        }

        public async Task AddAsync(OrderEntity orderEntity)
        {
            await _context.OrderEntities.AddAsync(orderEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderEntity orderEntity)
        {
            _context.OrderEntities.Update(orderEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int orderId)
        {
            var orderEntity = await GetByIdAsync(orderId);
            if (orderEntity != null)
            {
                _context.OrderEntities.Remove(orderEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
