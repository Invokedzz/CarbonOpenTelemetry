using Carbon.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class CarbonDbContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    
    public CarbonDbContext(DbContextOptions<CarbonDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarbonDbContext).Assembly);
    }
}