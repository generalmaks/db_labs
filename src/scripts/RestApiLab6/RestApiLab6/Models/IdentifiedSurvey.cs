using RestApiLab6.Entities;

namespace RestApiLab6.Models;

public class IdentifiedSurvey : Survey, IHasId
{
    public IdentifiedSurvey(int id, string title, string? description, DateTime creationDate, DateTime? closeDate,
        bool isChangable, bool isActive, int ownerId)
        : base(title, description, creationDate, closeDate, isChangable, isActive, ownerId)
    {
        Id = id;
    }

    public IdentifiedSurvey(SurveyEntity s)
        : this(s.Id, s.Title, s.Description, s.CreationDate, s.CloseDate, s.IsChangable, s.IsActive, s.OwnerId)
    {
    }

    public int Id { get; }
}