namespace API.Models;

public class YoungCustomer : Customer{

    public string? SchoolName { get; set; } 
    public required string ParentGuardianContact { get; set; } 
    public bool IsStudent { get; set; } 
}