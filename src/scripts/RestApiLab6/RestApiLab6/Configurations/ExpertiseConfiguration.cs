using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApiLab6.Entities;

namespace RestApiLab6.Configurations;

public class ExpertiseConfiguration : IEntityTypeConfiguration<ExpertiseEntity>
{
    public void Configure(EntityTypeBuilder<ExpertiseEntity> builder)
    {
        builder.ToTable("expertise");

        builder.HasKey(e => new { e.Id, e.CategoryId, e.UserId }).HasName("PK");

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");

        builder.Property(e => e.ExpertiseRate)
            .IsRequired()
            .HasColumnName("expertise_rate");

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnName("user_id");

        builder.Property(e => e.CategoryId)
            .IsRequired()
            .HasColumnName("category_id");

        builder.HasOne(e => e.UserEntity)
            .WithMany(u => u.Expertises)
            .HasForeignKey(e => e.UserId);

        builder.HasOne(e => e.CategoryEntity)
            .WithMany()
            .HasForeignKey(e => e.CategoryId);
    }
}