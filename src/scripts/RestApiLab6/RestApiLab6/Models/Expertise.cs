namespace RestApiLab6.Models;

public class Expertise
{
    public Expertise(double expertiseRate, int categoryId, int userId)
    {
        ExpertiseRate = expertiseRate;
        CategoryId = categoryId;
        UserId = userId;
    }

    public double ExpertiseRate { get; }
    public int CategoryId { get; }
    public int UserId { get; }
}