namespace Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

public interface IUser
{
    long Id { get; set; }

    UserRole Role { get; set; }

    string Name { get; set; }

    string Password { get; set; }
}