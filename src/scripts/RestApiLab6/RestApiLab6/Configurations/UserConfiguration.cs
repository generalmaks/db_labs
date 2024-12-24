using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApiLab6.Entities;

namespace RestApiLab6.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("user");

        builder.HasKey(u => u.Id)
            .HasName("PK");

        builder.Property(u => u.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(u => u.FirstName)
            .HasMaxLength(45)
            .IsRequired()
            .HasColumnName("first_name");

        builder.Property(u => u.LastName)
            .HasMaxLength(45)
            .IsRequired()
            .HasColumnName("last_name");

        builder.Property(u => u.Email)
            .HasMaxLength(45)
            .IsRequired()
            .HasColumnName("email");

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(45)
            .HasColumnName("phone_number");

        builder.Property(u => u.Password)
            .HasMaxLength(45)
            .IsRequired()
            .HasColumnName("password");

        builder.Property(u => u.IsAdmin)
            .HasColumnType("tinyint(1)")
            .IsRequired()
            .HasColumnName("is_admin");

        builder.Property(u => u.Description)
            .HasColumnType("text")
            .HasColumnName("description");

        builder.Property(u => u.Age)
            .HasColumnName("age");

        builder.Property(u => u.Gender)
            .HasMaxLength(45)
            .HasColumnName("gender");

        builder.Property(u => u.Company)
            .HasMaxLength(45)
            .HasColumnName("company");
    }
}