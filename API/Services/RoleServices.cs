using API.implementations.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Services;

public class RoleServices
{
    private readonly AppDbContext _context;

    public RoleServices(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetRoleByNameAsync(string roleName)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.Name.ToLower() == roleName.ToLower());
    } 
}
