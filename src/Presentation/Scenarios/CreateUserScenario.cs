using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios;

public class CreateUserScenario : IScenario
{
    private readonly IUserRepository _userService;

    public CreateUserScenario(IUserRepository userService)
    {
        _userService = userService;
    }

    public string Name => "Create new user";

    public void Run()
    {
        long userId = AnsiConsole.Ask<long>("Enter id");
        string userName = AnsiConsole.Ask<string>("Enter user name:");
        UserRole userRole = AnsiConsole.Prompt(
            new SelectionPrompt<UserRole>()
                .Title("Select user role:")
                .AddChoices(UserRole.User, UserRole.Admin));
        string userPassword = AnsiConsole.Ask<string>("Enter user password:");

        try
        {
            var newUser = new AtmUser(userId, userRole, userName, userPassword);
            _userService.CreateUserAsync(newUser).GetAwaiter().GetResult();

            AnsiConsole.MarkupLine($"[green]User '{userName}' successfully created![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]An error occurred: {ex.Message}[/]");
        }
    }
}
