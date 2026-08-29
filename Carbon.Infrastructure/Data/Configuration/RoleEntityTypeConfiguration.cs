using Carbon.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("CarbonRoles");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.Property(e => e.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasMany(e => e.Users)
            .WithMany(e => e.Roles);
    }
}