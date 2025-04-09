namespace API.Models.DTOs
{
    public class WarehouseResponseDto
    {
        public int WarehouseId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public List<AddItemToWarehouseDTO> Items { get; set; } // Reuse AddItemToWarehouseDTO
        public int BranchId { get; set; }
    }
}
