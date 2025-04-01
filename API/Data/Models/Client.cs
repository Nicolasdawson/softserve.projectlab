using System;
using System.Collections.Generic;

namespace API.Data.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Email { get; set; }

    public byte[] Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public bool Wholesale { get; set; }

    public DateOnly BirthDate { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }
}
