using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SandwellExampleSeedData.Models;

namespace SandwellExampleSeedData.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SandwellExampleSeedData.Models.HotelRoom> HotelRoom { get; set; } = default!;
        public DbSet<SandwellExampleSeedData.Models.Booking> Booking { get; set; } = default!;
    }
}
