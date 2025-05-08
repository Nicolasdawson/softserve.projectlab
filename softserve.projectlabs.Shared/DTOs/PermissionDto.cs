namespace softserve.projectlabs.Shared.DTOs
{
    public class PermissionDto : BaseDto
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; } = string.Empty;
        public string PermissionDescription { get; set; } = string.Empty;
    }
}
