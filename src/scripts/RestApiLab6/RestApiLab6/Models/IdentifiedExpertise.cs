using RestApiLab6.Entities;

namespace RestApiLab6.Models;

public class IdentifiedExpertise : Expertise, IHasId
{
    public IdentifiedExpertise(int id, double expertiseRate, int categoryId, int userId)
        : base(expertiseRate, categoryId, userId)
    {
        Id = id;
    }

    public IdentifiedExpertise(ExpertiseEntity e) : this(e.Id, e.ExpertiseRate, e.CategoryId, e.UserId)
    {
    }

    public int Id { get; }
}