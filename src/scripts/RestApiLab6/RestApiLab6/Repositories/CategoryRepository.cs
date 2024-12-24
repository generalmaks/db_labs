using Microsoft.EntityFrameworkCore;
using RestApiLab6.Entities;
using RestApiLab6.Models;
using RestApiLab6.MyDbContext;

namespace RestApiLab6.Repositories;

public class CategoryRepository
{
    private SurveyDbContext _dbContext;

    public CategoryRepository(SurveyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<IdentifiedCategory>> GetAll()
    {
        var categories = await _dbContext.Categories
            .Select(c => new IdentifiedCategory(c))
            .ToListAsync();

        return categories;
    }

    public async Task<IdentifiedCategory> Get(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);

        if (category is null)
            throw new NullReferenceException($"Category with id {id} could not be found");

        return new IdentifiedCategory(category);
    }

    public async Task<IdentifiedCategory> Add(Category category)
    {
        if (await _dbContext.Categories.AnyAsync(c => c.Name == category.Name))
            throw new Exception($"Category with name {category.Name} already exists");

        var categoryEntity = new CategoryEntity()
        {
            Name = category.Name,
            ParentId = category.ParentId
        };

        await _dbContext.Categories.AddAsync(categoryEntity);
        await _dbContext.SaveChangesAsync();

        return new IdentifiedCategory(categoryEntity);
    }

    public async Task UpdateName(int id, string name)
    {
        if (await _dbContext.Categories.AnyAsync(c => c.Name == name))
            throw new Exception($"Category with name {name} already exists");

        await _dbContext.Categories.Where(c => c.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.Name, name));
    }

    public async Task Delete(int id)
    {
        await _dbContext.Categories
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
    }
}