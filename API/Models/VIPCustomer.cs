namespace API.Models;

public class VIPCustomer : Customer
{
    public DateOnly MembershipStartDate { get; set; } 
    public required string VIPLevel { get; set; } 
    public decimal Discount { get; set; } 
}