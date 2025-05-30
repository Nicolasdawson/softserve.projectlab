using API.implementations.Infrastructure.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using API.DTO.DeliveryAddress;

namespace API.Services;

public class DeliveryAddressService
{
    private readonly AppDbContext _context;

    public DeliveryAddressService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DeliveryAddress> CreateAsync(DeliveryAddress address)
    {
        _context.DeliveryAddresses.Add(address);
        await _context.SaveChangesAsync();
        return address;
    }

    public async Task<DeliveryAddress> CreateFromRequestAsync(DeliveryAddressRequest request)
    {
        if (request == null)
        throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.CityName))
        throw new ArgumentException("CityName cannot be null or empty.", nameof(request.CityName));

        var regionExists = await _context.Regions.AnyAsync(r => r.Id == request.IdRegion);
        if (!regionExists)
            return null;


        // Busca ciudad por nombre (case-insensitive)
        var city = await _context.Cities
            .FirstOrDefaultAsync(c => c.Name.ToLower() == request.CityName.ToLower());

        if (city == null)
        {
            // Crea la ciudad nueva
            city = new City
            {
                Id = Guid.NewGuid(),
                Name = request.CityName,
                PostalCode = request.PostalCode ?? "", // Podrías pedir esto también si lo necesitas
                IdRegion = request.IdRegion
            };

            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
        }

        var address = new DeliveryAddress
        {
            Id = Guid.NewGuid(),
            StreetName = request.StreetName,
            StreetNumber = request.StreetNumber,
            StreetNameOptional = request.StreetNameOptional,
            IdCity = city.Id
        };

        _context.DeliveryAddresses.Add(address);
        await _context.SaveChangesAsync();

        return address;
    }


    public async Task<List<DeliveryAddress>> GetAllAsync()
    {
        return await _context.DeliveryAddresses.ToListAsync();
    }

    public async Task<DeliveryAddress?> GetByIdAsync(Guid id)
    {
        return await _context.DeliveryAddresses.FindAsync(id);
    }

    public async Task<DeliveryAddressResponse> MapToResponseAsync(DeliveryAddress address)
    {
        var city = await _context.Cities
            .Include(c => c.Region)
                .ThenInclude(r => r.Country)
            .FirstOrDefaultAsync(c => c.Id == address.IdCity);

        if (city == null) throw new Exception("City not found");

        return new DeliveryAddressResponse
        {
            Id = address.Id,
            StreetName = address.StreetName,
            StreetNumber = address.StreetNumber,
            StreetNameOptional = address.StreetNameOptional,
            CityName = city.Name,
            CityId = city.Id,
            RegionName = city.Region.Name,
            CountryName = city.Region.Country.Name
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var address = await _context.DeliveryAddresses.FindAsync(id);
        if (address == null || address.IsDeleted)
            return false;

        address.IsDeleted = true;
        _context.DeliveryAddresses.Update(address);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<DeliveryAddress>> GetByCustomerIdThroughOrdersAsync(int customerId)
    {
        var addressIds = await _context.Orders
            .Where(o => o.IdCustomer == customerId)
            .Select(o => o.IdDeliveryAddress)
            .Distinct()
            .ToListAsync();

        return await _context.DeliveryAddresses
            .Where(d => addressIds.Contains(d.Id) && !d.IsDeleted)
            .ToListAsync();
    }




}
