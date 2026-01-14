using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Holiday.Models;

namespace Holiday.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Holiday.Models.Destination> Destination { get; set; } = default!;
        public DbSet<Holiday.Models.Flight> Flight { get; set; } = default!;
        public DbSet<Holiday.Models.Travel> Travel { get; set; } = default!;
        public DbSet<Holiday.Models.ticket> ticket { get; set; } = default!;
    }
}
