using RestApiLab6.Entities;

namespace RestApiLab6.Models;

public class IdentifiedCategory : Category, IHasId
{
    public IdentifiedCategory(int id, string name, int? parentId = null) : base(name, parentId)
    {
        Id = id;
    }

    public IdentifiedCategory(CategoryEntity c) : this(c.Id, c.Name, c.ParentId)
    {
    }

    public int Id { get; }
}