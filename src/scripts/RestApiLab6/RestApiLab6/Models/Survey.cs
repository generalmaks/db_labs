namespace RestApiLab6.Models;

public class Survey
{
    public Survey(string title, string? description, DateTime creationDate, DateTime? closeDate, bool isChangable,
        bool isActive, int ownerId)
    {
        Title = title;
        Description = description;
        CreationDate = creationDate;
        CloseDate = closeDate;
        IsChangable = isChangable;
        IsActive = isActive;
        OwnerId = ownerId;
    }

    public string Title { get; }
    public string? Description { get; }
    public DateTime CreationDate { get; }
    public DateTime? CloseDate { get; }
    public bool IsChangable { get; }
    public bool IsActive { get; }
    public int OwnerId { get; }
}