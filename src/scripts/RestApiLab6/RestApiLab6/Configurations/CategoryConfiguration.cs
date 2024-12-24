using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApiLab6.Entities;

namespace RestApiLab6.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.ToTable("category");

        builder.HasKey(c => c.Id)
            .HasName("PK");

        builder.Property(c => c.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(c => c.Name)
            .HasMaxLength(45)
            .IsRequired()
            .HasColumnName("name");

        builder.Property(c => c.ParentId)
            .HasColumnName("parent_id");

        builder.HasOne(c => c.Parent)
            .WithMany()
            .HasForeignKey(c => c.ParentId);
    }
}