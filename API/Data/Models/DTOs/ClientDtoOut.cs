using System;

namespace API.Data.Models.DTOs
{
    public class ClientDtoOut
    {
        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public bool Wholesale { get; set; }

        public DateOnly BirthDate { get; set; }

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;
    }
}
