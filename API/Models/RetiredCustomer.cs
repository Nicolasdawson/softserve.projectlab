namespace API.Models;

public class RetiredCustomer : Customer{

    public DateOnly RetirementDate { get; set; } 
    public required string PensionSource { get; set; } 
    public bool IsReceivingBenefits { get; set; }
}