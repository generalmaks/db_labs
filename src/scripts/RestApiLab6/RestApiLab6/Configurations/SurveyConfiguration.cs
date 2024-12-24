using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApiLab6.Entities;

namespace RestApiLab6.Configurations;

public class SurveyConfiguration : IEntityTypeConfiguration<SurveyEntity>
{
    public void Configure(EntityTypeBuilder<SurveyEntity> builder)
    {
        builder.ToTable("survey");

        builder.HasKey(s => s.Id)
            .HasName("PK");

        builder.Property(s => s.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(s => s.Title)
            .HasMaxLength(45)
            .IsRequired()
            .HasColumnName("title");

        builder.Property(s => s.Description)
            .HasColumnType("text")
            .HasColumnName("description");

        builder.Property(s => s.CreationDate)
            .IsRequired()
            .HasColumnName("creation_date");

        builder.Property(s => s.CloseDate)
            .HasColumnName("close_date");

        builder.Property(s => s.IsChangable)
            .IsRequired()
            .HasColumnType("tinyint(1)")
            .HasColumnName("is_changable");

        builder.Property(s => s.IsActive)
            .IsRequired()
            .HasColumnType("tinyint(1)")
            .HasColumnName("is_active");

        builder.Property(s => s.OwnerId)
            .HasColumnName("owner_id");

        builder.HasOne(s => s.Owner)
            .WithMany(u => u.Surveys)
            .HasForeignKey(s => s.OwnerId);
    }
}