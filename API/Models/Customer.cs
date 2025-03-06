namespace API.Models;

public class Customer
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateOnly BirthDate { get; set; }
    
    public string Email { get; set; }
    
    public LineOfCredit? LineOfCredit { get; set; }

    public Customer() { }
    public Customer(string firstName, string lastName, DateOnly birthDate, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Email = email;
    }

    public virtual void ShowBenefits()
    {
        Console.WriteLine("Standard customer benefits apply.");
    }
}

public class PremiumCustomer : Customer
{
    public decimal DiscountRate { get; set; }

    public PremiumCustomer(string firstName, string lastName, DateOnly birthDate, string email, decimal discountRate)
        : base(firstName, lastName, birthDate, email)
    {
        DiscountRate = discountRate;
    }

    public override void ShowBenefits()
    {
        Console.WriteLine("Premium benefits: Lower interest rates and higher credit limits.");
    }
}
public class YoungCustomer : Customer
{
    public bool RequiresGuardian { get; set; }

    public YoungCustomer(string firstName, string lastName, DateOnly birthDate, string email, bool requiresGuardian)
        : base(firstName, lastName, birthDate, email)
    {
        RequiresGuardian = requiresGuardian;
    }

    public override void ShowBenefits()
    {
        Console.WriteLine("Young customer: Lower credit limit and guardian approval required.");
    }
}

public class SeniorCustomer : Customer
{
    public int RetirementAge { get; set; }

    public SeniorCustomer(string firstName, string lastName, DateOnly birthDate, string email, int retirementAge)
        : base(firstName, lastName, birthDate, email)
    {
        RetirementAge = retirementAge;
    }

    public override void ShowBenefits()
    {
        Console.WriteLine("Senior benefits: Special discounts and financial advisory services.");
    }
}