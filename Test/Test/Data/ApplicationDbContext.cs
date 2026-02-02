using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Test.Models.Accessibility> Accessibility { get; set; } = default!;
        public DbSet<Test.Models.AccountInformation> AccountInformation { get; set; } = default!;
        public DbSet<Test.Models.Booking> Booking { get; set; } = default!;
        public DbSet<Test.Models.Room> Room { get; set; } = default!;
    }
}
