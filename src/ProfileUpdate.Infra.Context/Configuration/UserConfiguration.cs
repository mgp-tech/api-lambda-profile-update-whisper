using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileUpdate.Core.Domain.Entity;

namespace ProfileUpdate.Infra.Context.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Creator).IsRequired();
        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Nickname).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.DocumentPrimary).IsRequired().HasMaxLength(30);
        builder.Property(u => u.DocumentSecondary).HasMaxLength(30);
        builder.Property(u => u.Phone).IsRequired().HasMaxLength(20);
        builder.Property(u => u.Birthday).IsRequired().HasMaxLength(10);
        builder.Property(u => u.Country).IsRequired().HasMaxLength(50);
        builder.Property(u => u.PostalCode).IsRequired().HasMaxLength(20);
        builder.Property(u => u.Address).IsRequired().HasMaxLength(200);
        builder.Property(u => u.Complement).HasMaxLength(100);
        builder.Property(u => u.Neighborhood).IsRequired().HasMaxLength(100);
        builder.Property(u => u.City).IsRequired().HasMaxLength(100);
        builder.Property(u => u.State).IsRequired().HasMaxLength(50);
    }
}