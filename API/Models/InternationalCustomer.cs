namespace API.Models
{
    public class InternationalCustomer: Customer
    {
        public string Country { get; set; }  // País de origen
        public string Currency { get; set; }  // Moneda preferida para transacciones
        public string CountryCode { get; set; }  // Código de país (ej., "US" para Estados Unidos)
    }
}
