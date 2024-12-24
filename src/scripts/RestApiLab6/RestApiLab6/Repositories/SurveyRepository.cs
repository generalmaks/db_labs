using Microsoft.EntityFrameworkCore;
using RestApiLab6.Entities;
using RestApiLab6.Models;
using RestApiLab6.MyDbContext;

namespace RestApiLab6.Repositories;

public class SurveyRepository
{
    private SurveyDbContext _dbContext;

    public SurveyRepository(SurveyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<IdentifiedSurvey>> GetAllByOwner(int ownerId)
    {
        return await _dbContext.Surveys
            .Where(s => s.OwnerId == ownerId)
            .Select(s => new IdentifiedSurvey(s))
            .ToListAsync();
    }

    public async Task<List<IdentifiedSurvey>> GetAllWithTitlePart(string titlePart)
    {
        return await _dbContext.Surveys
            .Where(s => s.Title.Contains(titlePart))
            .Select(s => new IdentifiedSurvey(s))
            .ToListAsync();
    }

    public async Task<Survey> GetById(int id)
    {
        var survey = await _dbContext.Surveys.FindAsync(id);
        if (survey is null)
            throw new Exception($"Survey with id {id} not found");

        return new IdentifiedSurvey(survey);
    }

    public async Task<IdentifiedSurvey> Add(Survey survey)
    {
        var createdSurvey = new SurveyEntity()
        {
            Title = survey.Title,
            Description = survey.Description,
            CreationDate = survey.CreationDate,
            CloseDate = survey.CloseDate,
            IsChangable = survey.IsChangable,
            IsActive = survey.IsActive,
            OwnerId = survey.OwnerId
        };

        await _dbContext.Surveys.AddAsync(createdSurvey);
        await _dbContext.SaveChangesAsync();

        return new IdentifiedSurvey(createdSurvey);
    }

    public async Task UpdateTitle(int id, string title)
    {
        await _dbContext.Surveys
            .Where(s => s.Id == id)
            .ExecuteUpdateAsync(sp => sp
                .SetProperty(s => s.Title, title));
    }

    public async Task UpdateDescription(int id, string description)
    {
        await _dbContext.Surveys
            .Where(s => s.Id == id)
            .ExecuteUpdateAsync(sp => sp
                .SetProperty(s => s.Description, description));
    }

    public async Task UpdateActivity(int id, bool isActive)
    {
        await _dbContext.Surveys
            .Where(s => s.Id == id)
            .ExecuteUpdateAsync(sp => sp
                .SetProperty(s => s.IsActive, isActive));
    }

    public async Task Delete(int id)
    {
        await _dbContext.Surveys
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();
    }
}