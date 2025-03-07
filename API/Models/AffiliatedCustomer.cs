namespace API.Models
{
    public class AffiliatedCustomer : Customer
    {
        public const decimal SomeQuantity = 0.5M; // Cantidad pagada por cada customer referido
        public string AffiliateCode { get; set; }

        private int referredCount = 0;

        public void NewReferredClient()
        {
            referredCount++;
            this.LineOfCredit.Balance += SomeQuantity;
        }
    }
}
