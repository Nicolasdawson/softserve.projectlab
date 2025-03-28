namespace API.Models;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateOnly BirthDate { get; set; }
    
    public string Email { get; set; }
    
    public LineOfCredit? LineOfCredit { get; set; }

    public virtual string GetCustomerType()
    {
        return "Regular";
    }

    // TODONE: we need another type of customer and use this one as base, use inheritance 
}

public class SilverCustomer : Customer
{
    public double DiscountPercentage { get; set; } = 5.0;

    public override string GetCustomerType()
    {
        return "Silver";
    }
}

public class GoldCustomer : Customer
{
    public double DiscountPercentage { get; set; } = 10.0;
    public int RewardPoints { get; set; } = 1000;

    public override string GetCustomerType()
    {
        return "Gold";
    }
}

public class PlatinumCustomer : Customer
{
    public double DiscountPercentage { get; set; } = 15.0;
    public int RewardPoints { get; set; } = 5000;
    public bool HasExclusiveSupport { get; set; } = true;

    public override string GetCustomerType()
    {
        return "Platinum";
    }
}