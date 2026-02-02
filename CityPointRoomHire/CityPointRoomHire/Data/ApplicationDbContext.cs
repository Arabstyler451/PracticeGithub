using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CityPointRoomHire.Models;

namespace CityPointRoomHire.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CityPointRoomHire.Models.Accessibility> Accessibility { get; set; } = default!;
        public DbSet<CityPointRoomHire.Models.AccountInformation> AccountInformation { get; set; } = default!;
        public DbSet<CityPointRoomHire.Models.Booking> Booking { get; set; } = default!;
        public DbSet<CityPointRoomHire.Models.Room> Room { get; set; } = default!;
    }
}
