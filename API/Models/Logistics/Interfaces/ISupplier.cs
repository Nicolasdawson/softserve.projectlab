namespace API.Models.Logistics.Interfaces
{
    public interface ISupplier
    {
        int SupplierId { get; set; }
        string Name { get; set; }
        string ContactInfo { get; set; }

        Result<ISupplier> AddSupplier(ISupplier supplier);
        Result<ISupplier> UpdateSupplier(ISupplier supplier);
        Result<ISupplier> GetSupplierById(int supplierId);
        Result<List<ISupplier>> GetAllSuppliers();
        Result<bool> DeleteSupplier(int supplierId);
    }
}
