namespace API.Models
{
    public class premiumCustomer : Customer
    {
        public DateOnly MembershipExpiration { get; set; }

        public decimal ExclusiveDiscount { get; set; }
    }
}
