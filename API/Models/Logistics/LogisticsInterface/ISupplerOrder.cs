namespace API.Models.Logistics.LogisticsInterface
{
    public interface ISupplierOrder
    {
        Result<ISupplierOrder> AddSupplierOrder(ISupplierOrder supplierOrder);
        Result<ISupplierOrder> UpdateSupplierOrder(ISupplierOrder supplierOrder);
        Result<ISupplierOrder> GetSupplierOrderById(int orderId);
        Result<List<ISupplierOrder>> GetAllSupplierOrders();
        Result<bool> DeleteSupplierOrder(int orderId);
    }
}
