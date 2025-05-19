using API.Models;

namespace API.Services;

public interface IPendingRegistrationService
{
    Task<bool> ExistsByEmailAsync(string email);
    
    Task CreateAsync(PendingRegistration registration);

    Task<PendingRegistration?> GetByEmailAsync(string email);

    Task<bool> DeleteByIdAsync(Guid id);
}
