namespace softserve.projectlabs.Shared.DTOs.Category
{
    public class CategoryDto : BaseDto
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public bool CategoryStatus { get; set; }
    }
}
