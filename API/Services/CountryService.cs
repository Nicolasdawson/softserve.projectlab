using API.Models;
using API.DTO;
using Microsoft.EntityFrameworkCore;
using API.implementations.Infrastructure.Data;

namespace API.Services;

public class CountryService
{
    private readonly AppDbContext _context;

    public CountryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CountryDto>> GetAllAsync()
    {
        return await _context.Countries
            .Select(c => new CountryDto { Id = c.Id, Name = c.Name })
            .ToListAsync();
    }

    public async Task<IEnumerable<RegionDto>> GetRegionsByCountryIdAsync(Guid countryId)
    {
        return await _context.Regions
            .Where(r => r.IdCountry  == countryId)
            .Select(r => new RegionDto { Id = r.Id, Name = r.Name })
            .ToListAsync();
    }
}