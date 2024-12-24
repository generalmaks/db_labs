using RestApiLab6.Entities;

namespace RestApiLab6.Models;

public class IdentifiedUser : User, IHasId
{
    public IdentifiedUser(int id, string firstName, string lastName, string email, string? phoneNumber, string password,
        bool isAdmin, string? description, int? age, string? gender, string? company)
        : base(firstName, lastName, email, phoneNumber, password, isAdmin, description, age, gender, company)
    {
        Id = id;
    }

    public IdentifiedUser(UserEntity u) : this(u.Id, u.FirstName, u.LastName, u.Email, u.PhoneNumber, u.Password,
        u.IsAdmin, u.Description, u.Age, u.Gender, u.Company)
    {
    }

    public int Id { get; }
}