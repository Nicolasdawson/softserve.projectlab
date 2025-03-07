namespace API.Models
{
    public class PremiumCustomer : Customer
    {
        public DateOnly MembershipExpiration { get; set; }

        public decimal ExclusiveDiscount { get; set; }
    }
}
