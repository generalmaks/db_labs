namespace RestApiLab6.Models;

public class User
{
    public User(string firstName, string lastName, string email, string phoneNumber, string password, bool isAdmin,
        string description,
        int? age, string gender, string company)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        IsAdmin = isAdmin;
        Description = description;
        Age = age;
        Gender = gender;
        Company = company;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string? PhoneNumber { get; }
    public string Password { get; }
    public bool IsAdmin { get; }
    public string? Description { get; }
    public int? Age { get; }
    public string? Gender { get; }
    public string? Company { get; }
}