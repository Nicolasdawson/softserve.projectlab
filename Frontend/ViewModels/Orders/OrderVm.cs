namespace Frontend.ViewModels.Orders
{
    public class OrderVm
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public decimal OrderTotal { get; set; }

        public DateTime OrderPlaced { get; set; } 

        public string UserEmail { get; set; }
    }
}
