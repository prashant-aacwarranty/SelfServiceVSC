using System;
using System.Collections.Generic;

namespace SelfServiceVSC.DAL;

public partial class Customer
{
    public long Id { get; set; }

    public string Vin { get; set; } = null!;

    public string Coverage { get; set; } = null!;

    public int Deductible { get; set; }

    public DateTime CoverageExpirationDate { get; set; }

    public int CoverageExpirationMileage { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Street1 { get; set; } = null!;

    public string? Street2 { get; set; }

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public decimal Amount { get; set; }

    public bool IsPurchased { get; set; }

    public DateTime CreatedDate { get; set; }
    public int RateId { get; set; }
    public bool IsSwitched { get; set; }
}
