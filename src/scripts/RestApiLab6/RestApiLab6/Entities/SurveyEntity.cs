namespace RestApiLab6.Entities;

public class SurveyEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? CloseDate { get; set; }
    public bool IsChangable { get; set; }
    public bool IsActive { get; set; }
    public int OwnerId { get; set; }
    public UserEntity Owner { get; set; }
}