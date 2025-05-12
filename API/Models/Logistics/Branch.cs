namespace API.Models.Logistics
{
    public class Branch
    {
        public int BranchId { get; private set; }
        public string BranchName { get; private set; }
        public string BranchCity { get; private set; }
        public string BranchRegion { get; private set; }
        public string BranchAddress { get; private set; }
        public string BranchContactNumber { get; private set; }
        public string BranchContactEmail { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool IsDeleted { get; private set; }

        public Branch(
            int branchId,
            string branchName,
            string branchCity,
            string branchRegion,
            string branchAddress,
            string branchContactNumber,
            string branchContactEmail,
            DateTime createdAt,
            DateTime updatedAt,
            bool isDeleted = false)
        {
            BranchId = branchId;
            BranchName = branchName;
            BranchCity = branchCity;
            BranchRegion = branchRegion;
            BranchAddress = branchAddress;
            BranchContactNumber = branchContactNumber;
            BranchContactEmail = branchContactEmail;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            IsDeleted = isDeleted;
        }

        public void UpdateDetails(string name, string city, string region, string address, string contactNumber, string contactEmail)
        {
            BranchName = name;
            BranchCity = city;
            BranchRegion = region;
            BranchAddress = address;
            BranchContactNumber = contactNumber;
            BranchContactEmail = contactEmail;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsDeleted()
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
