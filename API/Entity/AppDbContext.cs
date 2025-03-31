
using Microsoft.EntityFrameworkCore;

namespace API.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }


    }
}
