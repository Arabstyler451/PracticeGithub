using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ChakdeLife.Models;

namespace ChakdeLife.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<ChakdeLife.Models.Category> Category { get; set; } = default!;
        public DbSet<ChakdeLife.Models.Order> Order { get; set; } = default!;
        public DbSet<ChakdeLife.Models.Product> Product { get; set; } = default!;
        public DbSet<ChakdeLife.Models.WalkingGroup> WalkingGroup { get; set; } = default!;
        public DbSet<ChakdeLife.Models.WalkingGroupMember> WalkingGroupMember { get; set; } = default!;
    }
}
