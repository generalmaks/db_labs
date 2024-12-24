using Microsoft.EntityFrameworkCore;
using RestApiLab6.Entities;
using RestApiLab6.Models;
using RestApiLab6.MyDbContext;

namespace RestApiLab6.Repositories;

public class UserRepository
{
    private SurveyDbContext _dbContext;

    public UserRepository(SurveyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<IdentifiedUser>> GetAll()
    {
        return await _dbContext.Users
            .Select(u => new IdentifiedUser(u))
            .ToListAsync();
    }

    public async Task<IdentifiedUser> Get(int id)
    {
        var user = await _dbContext.Users
            .FindAsync(id);

        if (user is null)
            throw new Exception($"User with id {id} not found");

        return new IdentifiedUser(user);
    }

    public async Task<IdentifiedUser> Create(User user)
    {
        if (await _dbContext.Users.AnyAsync(u => u.Email == user.Email))
            throw new Exception($"Email {user.Email} already taken");

        var phone = user.PhoneNumber;

        if (!string.IsNullOrWhiteSpace(phone))
        {
            if (await _dbContext.Users.AnyAsync(u => u.PhoneNumber == phone))
                throw new Exception($"PhoneNumber {user.PhoneNumber} already taken");
        }

        var userEntity = new UserEntity()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Password = user.Password,
            IsAdmin = user.IsAdmin,
            Description = user.Description,
            Age = user.Age
        };

        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();

        var created = new IdentifiedUser(userEntity);

        return created;
    }

    public async Task UpdatePhoneNumber(int id, string phoneNumber)
    {
        if (await _dbContext.Users.AnyAsync(u => u.PhoneNumber == phoneNumber))
            throw new Exception($"PhoneNumber {phoneNumber} already taken");

        var user = await _dbContext.Users.FindAsync(id);

        if (user is null)
            throw new Exception($"User with id {id} not found");

        user.PhoneNumber = phoneNumber;
    }

    public async Task UpdatePassword(int id, string password)
    {
        await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Password, password));
    }

    public async Task Delete(int id)
    {
        await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();
    }
}