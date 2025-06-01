namespace API.DTO.DeliveryAddress;

public class DeliveryAddressRequest
{
    public string StreetName { get; set; } = default!;
    public string StreetNumber { get; set; } = default!;
    public string? StreetNameOptional { get; set; } = string.Empty;

    public string CityName { get; set; } = default!;
    public Guid IdRegion { get; set; } // El cliente elige región desde un select
    public string? PostalCode { get; set; }
}

public class DeliveryAddressResponse
{
    public Guid Id { get; set; }

    public string StreetName { get; set; } = default!;
    public string StreetNumber { get; set; } = default!;
    public string StreetNameOptional { get; set; } = string.Empty;

    public string CityName { get; set; } = default!;
    public Guid CityId { get; set; }
    public string RegionName { get; set; } = default!;
    public string CountryName { get; set; } = default!;
}

