namespace Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

public class AtmUser : IUser
{
    public long Id { get; set; }

    public UserRole Role { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public AtmUser(long id, UserRole role, string name, string password)
    {
        Id = id;
        Role = role;
        Name = name;
        Password = password;
    }
}