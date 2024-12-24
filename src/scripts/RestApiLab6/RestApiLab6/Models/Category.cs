namespace RestApiLab6.Models;

public class Category
{
    public Category(string name, int? parentId)
    {
        Name = name;
        ParentId = parentId;
    }

    public string Name { get; }
    public int? ParentId { get; }
}