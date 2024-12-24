namespace RestApiLab6.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
    public string? Description { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string? Company { get; set; }
    public List<ExpertiseEntity> Expertises { get; set; }
    public List<SurveyEntity> Surveys { get; set; }
}