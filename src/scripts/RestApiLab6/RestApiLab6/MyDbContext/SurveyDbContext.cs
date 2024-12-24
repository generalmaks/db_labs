using Microsoft.EntityFrameworkCore;
using RestApiLab6.Configurations;
using RestApiLab6.Entities;

namespace RestApiLab6.MyDbContext;

public class SurveyDbContext : DbContext
{
    public SurveyDbContext(DbContextOptions<SurveyDbContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ExpertiseEntity> Expertises { get; set; }
    public DbSet<SurveyEntity> Surveys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ExpertiseConfiguration());
        modelBuilder.ApplyConfiguration(new SurveyConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}