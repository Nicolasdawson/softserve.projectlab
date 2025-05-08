using API.Data.Entities;

namespace API.Data.Repositories.LogisticsRepositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderEntity?> GetByIdAsync(int orderId);
        Task<List<OrderEntity>> GetAllAsync();
        Task AddAsync(OrderEntity orderEntity);
        Task UpdateAsync(OrderEntity orderEntity);
        Task DeleteAsync(int orderId);
    }
}
