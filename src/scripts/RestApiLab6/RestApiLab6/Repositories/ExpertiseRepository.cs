using Microsoft.EntityFrameworkCore;
using RestApiLab6.Entities;
using RestApiLab6.Models;
using RestApiLab6.MyDbContext;

namespace RestApiLab6.Repositories;

public class ExpertiseRepository
{
    private SurveyDbContext _dbContext;

    public ExpertiseRepository(SurveyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<IdentifiedExpertise>> GetAllByUser(int userId)
    {
        return await _dbContext.Expertises
            .Where(e => e.UserId == userId)
            .Select(e => new IdentifiedExpertise(e))
            .ToListAsync();
    }

    public async Task<IdentifiedExpertise> Get(int id)
    {
        var expertise = await _dbContext.Expertises.FindAsync(id);

        if (expertise == null)
            throw new Exception($"Expertise with id {id} not found");

        return new IdentifiedExpertise(expertise);
    }

    public async Task<IdentifiedExpertise> Add(Expertise expertise)
    {
        bool isExist = await _dbContext.Expertises
            .AnyAsync(e =>
                e.UserId == expertise.UserId
                && e.CategoryId == expertise.CategoryId);

        if (isExist)
            throw new Exception("Expertise already exists");

        var createdExpertise = new ExpertiseEntity()
        {
            ExpertiseRate = expertise.ExpertiseRate,
            CategoryId = expertise.CategoryId,
            UserId = expertise.UserId
        };

        await _dbContext.Expertises.AddAsync(createdExpertise);
        await _dbContext.SaveChangesAsync();

        return new IdentifiedExpertise(createdExpertise);
    }

    public async Task UpdateRate(int id, double rate)
    {
        await _dbContext.Expertises
            .Where(e => e.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(e => e.ExpertiseRate, rate));
    }

    public async Task Delete(int id)
    {
        await _dbContext.Expertises.Where(e => e.Id == id)
            .ExecuteDeleteAsync();
    }
}