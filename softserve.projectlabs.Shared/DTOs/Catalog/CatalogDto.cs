namespace softserve.projectlabs.Shared.DTOs.Catalog
{
    public class CatalogDto : BaseDto
    {
        public int? CatalogId { get; set; }
        public string CatalogName { get; set; } = string.Empty;
        public string CatalogDescription { get; set; } = string.Empty;
        public bool CatalogStatus { get; set; }

        // List of Category IDs to be associated with the catalog.
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}