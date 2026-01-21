using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToKaFitness.Models;

namespace ToKaFitness.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ToKaFitness.Models.GymCustomer> GymCustomer { get; set; } = default!;
        public DbSet<ToKaFitness.Models.GymPass> GymPass { get; set; } = default!;
    }
}
