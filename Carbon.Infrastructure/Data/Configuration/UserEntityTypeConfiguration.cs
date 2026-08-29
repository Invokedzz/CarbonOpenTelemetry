using Carbon.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("CarbonUsers");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(e => e.Username)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(e => e.Email)
            .HasMaxLength(60)
            .IsRequired();
        
        builder.Property(e => e.Password)
            .HasMaxLength(128)
            .IsRequired();

        builder.HasMany(e => e.Roles)
            .WithMany(e => e.Users)
            .UsingEntity(e => e.ToTable("UserRoles"));
        
        builder.Property(e => e.IsActive)
            .HasDefaultValue(true);
    }
}