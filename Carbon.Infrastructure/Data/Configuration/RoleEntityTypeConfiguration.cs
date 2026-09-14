using Carbon.Domain.Contracts.Services.Authentication;
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
        
        builder.HasData(
            new Role
            {
                Id = Guid.Parse("7f3a9c21-6b84-4d17-a5e2-91c7b8f04d36"),
                Name = nameof(Roles.General),
                Description = "General role"
            },
            new Role
            {
                Id = Guid.Parse("c2e51a79-3f06-48bd-9c42-7a1e5d83f6b0"),
                Name = nameof(Roles.Director),
                Description = "Director role"
            },
            new Role
            {
                Id = Guid.Parse("a84d2f63-91c7-4be5-b038-6e2a7f19c5d4"),
                Name = nameof(Roles.Admin),
                Description = "Admin role"
            }
        );
    }
}