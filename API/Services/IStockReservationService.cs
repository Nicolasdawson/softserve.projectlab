namespace API.Services;

public interface IStockReservationService
{
    Task<bool> TryReserveStockAsync(Guid productId, int quantity);
    Task ReleaseReservationAsync(Guid productId, int quantity);
    Task SetStockAsync(Guid productId, int quantity);
    Task<int> GetAvailableStockAsync(Guid productId);
}
