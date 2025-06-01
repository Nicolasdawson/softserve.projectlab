using API.DTO;
using API.implementations.Infrastructure.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class PendingRegistrationService : IPendingRegistrationService
{
    private readonly AppDbContext _context;
        
    public PendingRegistrationService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.PendingRegistrations
            .AnyAsync(p => p.Email == email);
    }

    public async Task CreateAsync(PendingRegistration registration)
    {
        await _context.PendingRegistrations.AddAsync(registration);
        await _context.SaveChangesAsync();
    }

    public async Task<PendingRegistration?> GetByEmailAsync(string email)
    {
        return await _context.PendingRegistrations.FirstOrDefaultAsync(p => p.Email == email);
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var registration = await _context.PendingRegistrations.FindAsync(id);
        if (registration == null)
            return false;
        _context.PendingRegistrations.Remove(registration);
        
        await _context.SaveChangesAsync();
        return true;
    }
   
}
