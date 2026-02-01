using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CityHome.Models;

namespace CityHome.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CityHome.Models.Accessibility> Accessibility { get; set; } = default!;
        public DbSet<CityHome.Models.AccountInformation> AccountInformation { get; set; } = default!;
        public DbSet<CityHome.Models.Booking> Booking { get; set; } = default!;
        public DbSet<CityHome.Models.Room> Room { get; set; } = default!;
    }
}
