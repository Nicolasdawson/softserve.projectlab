namespace API.Models;

public class Customer
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateOnly BirthDate { get; set; }
    
    public string Email { get; set; }
    
    public LineOfCredit? LineOfCredit { get; set; }
    
}