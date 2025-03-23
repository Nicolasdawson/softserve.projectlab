using API.Models.IntAdmin.Interfaces;

namespace API.Models.IntAdmin
{
    public class Item : IItem
    {
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int TotalQuantity { get; set; }
        public decimal UnitCost { get; set; }
        public string ProductCurrency { get; set; }
        public decimal MarginGain { get; set; }
        public decimal AdditionalTax { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Discount { get; set; }
        public string StatusProduct { get; set; }

        public Item(string sku, string productName, string productDescription, int totalQuantity,
                    decimal unitCost, string productCurrency, decimal marginGain, decimal additionalTax,
                    decimal salePrice, decimal discount, string statusProduct)
        {
            Sku = sku;
            ProductName = productName;
            ProductDescription = productDescription;
            TotalQuantity = totalQuantity;
            UnitCost = unitCost;
            ProductCurrency = productCurrency;
            MarginGain = marginGain;
            AdditionalTax = additionalTax;
            SalePrice = salePrice;
            Discount = discount;
            StatusProduct = statusProduct;
        }

        // Constructor por defecto para casos de serialización u otras necesidades.
        public Item() { }

        public Result<IItem> AddItem(IItem item)
        {
            // Lógica para agregar un nuevo item (ej. guardar en base de datos o colección)
            return Result<IItem>.Success(item);
        }

        public Result<IItem> UpdateItem(IItem item)
        {
            // Lógica para actualizar un item existente
            return Result<IItem>.Success(item);
        }

        public Result<IItem> GetItemBySku(string sku)
        {
            // Lógica para obtener un item por su SKU
            var item = new Item(sku, "Producto de Ejemplo", "Descripción del producto de ejemplo",
                                100, 10.00m, "USD", 0.20m, 0.05m, 15.00m, 0.00m, "Disponible");
            return Result<IItem>.Success(item);
        }

        public Result<List<IItem>> GetAllItems()
        {
            // Lógica para obtener todos los items
            var items = new List<IItem>
            {
                new Item("SKU001", "Producto 1", "Descripción 1", 100, 10.00m, "USD", 0.20m, 0.05m, 15.00m, 0.00m, "Disponible"),
                new Item("SKU002", "Producto 2", "Descripción 2", 200, 20.00m, "USD", 0.25m, 0.10m, 30.00m, 0.05m, "Disponible")
            };
            return Result<List<IItem>>.Success(items);
        }

        public Result<bool> RemoveItem(string sku)
        {
            // Lógica para eliminar un item por su SKU
            return Result<bool>.Success(true);
        }
    }
}
