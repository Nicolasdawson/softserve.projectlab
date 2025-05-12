namespace API.Models.Logistics
{
    public class Supplier
    {
        public int SupplierId { get; private set; }
        public string Name { get; private set; }
        public string ContactNumber { get; private set; }
        public string ContactEmail { get; private set; }
        public string Address { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Supplier(
            int supplierId,
            string name,
            string contactNumber,
            string contactEmail,
            string address,
            bool isDeleted = false,
            DateTime? createdAt = null,
            DateTime? updatedAt = null)
        {
            SupplierId = supplierId;
            Name = name;
            ContactNumber = contactNumber;
            ContactEmail = contactEmail;
            Address = address;
            IsDeleted = isDeleted;
            CreatedAt = createdAt ?? DateTime.UtcNow;
            UpdatedAt = updatedAt ?? DateTime.UtcNow;
        }

        public void UpdateDetails(string name, string contactNumber, string contactEmail, string address)
        {
            Name = name;
            ContactNumber = contactNumber;
            ContactEmail = contactEmail;
            Address = address;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsDeleted()
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsActive()
        {
            IsDeleted = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
