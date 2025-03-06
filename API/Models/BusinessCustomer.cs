namespace API.Models
{
    public class BusinessCustomer: Customer
    {
        public string CompanyName { get; set; }  // Nombre de la empresa
        public string TaxIdentificationNumber { get; set; }  // Número de identificación fiscal
        public string BusinessAddress { get; set; }  // Dirección de la empresa
    }
}
