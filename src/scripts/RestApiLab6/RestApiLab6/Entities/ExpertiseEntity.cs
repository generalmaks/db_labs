namespace RestApiLab6.Entities;

public class ExpertiseEntity
{
    public int Id { get; set; }
    public double ExpertiseRate { get; set; }
    public int CategoryId { get; set; }
    public CategoryEntity CategoryEntity { get; set; }
    public int UserId { get; set; }
    public UserEntity UserEntity { get; set; }
}