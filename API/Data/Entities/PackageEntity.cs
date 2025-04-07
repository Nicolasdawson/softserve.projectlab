using System;
using System.Collections.Generic;

namespace API.Data.Entities;

public partial class PackageEntity
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime SaleDate { get; set; }

    public string Status { get; set; } = null!;

    public string? ContractId { get; set; }

    public int ContractTermMonths { get; set; }

    public DateTime ContractStartDate { get; set; }

    public decimal MonthlyFee { get; set; }

    public decimal SetupFee { get; set; }

    public decimal DiscountAmount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string? ShippingAddress { get; set; }

    public string? TrackingNumber { get; set; }

    public DateTime? EstimatedDeliveryDate { get; set; }

    public DateTime? ActualDeliveryDate { get; set; }

    public bool IsRenewal { get; set; }

    public int? PreviousPackageId { get; set; }

    public int CustomerId { get; set; }

    public virtual CustomerEntity Customer { get; set; } = null!;

    public virtual ICollection<PackageItemEntity> PackageItemEntities { get; set; } = new List<PackageItemEntity>();

    public virtual ICollection<PackageNoteEntity> PackageNoteEntities { get; set; } = new List<PackageNoteEntity>();
}
