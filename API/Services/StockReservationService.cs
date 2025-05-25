using StackExchange.Redis;

namespace API.Services
{
    public class StockReservationService
    {
        private readonly IDatabase _db;

        public StockReservationService(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        private string GetKey(Guid productId) => $"stock:{productId}";

        public async Task<bool> TryReserveStockAsync(Guid productId, int quantity)
        {
            Console.WriteLine($"Intentando reservar {quantity} unidades del producto {productId} en Redis");
            var key = GetKey(productId);
            var currentStock = (int?)await _db.StringGetAsync(key) ?? 0;

            if (currentStock < quantity)
                return false;

            await _db.StringDecrementAsync(key, quantity);
            await _db.KeyExpireAsync(key, TimeSpan.FromMinutes(15)); // TTL
            return true;
        }

        public async Task ReleaseReservationAsync(Guid productId, int quantity)
        {
            var key = GetKey(productId);
            await _db.StringIncrementAsync(key, quantity);
        }

        public async Task SetStockAsync(Guid productId, int quantity)
        {
            var key = GetKey(productId);
            await _db.StringSetAsync(key, quantity);
        }

        public async Task<int> GetAvailableStockAsync(Guid productId)
        {
            var key = GetKey(productId);
            return (int?)await _db.StringGetAsync(key) ?? 0;
        }
    }
}
