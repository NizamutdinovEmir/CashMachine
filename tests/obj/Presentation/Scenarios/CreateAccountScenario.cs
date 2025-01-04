using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios;

public class CreateAccountScenario : IScenario
{
    private readonly IAccountRepository _accountService;
    private readonly IUserRepository _userService;

    public CreateAccountScenario(IAccountRepository accountService, IUserRepository userService)
    {
        _accountService = accountService;
        _userService = userService;
    }

    public string Name => "Create new account";

    public void Run()
    {
        IEnumerable<IUser> users = _userService.GetAllUsersAsync().GetAwaiter().GetResult();
        if (users == null || !users.Any())
        {
            AnsiConsole.MarkupLine("[red]No users available to create an account for![/]");
            return;
        }

        SelectionPrompt<IUser> userSelector = new SelectionPrompt<IUser>()
            .Title("Select a user to create an account for:")
            .AddChoices(users)
            .UseConverter(u => $"{u.Name} (ID: {u.Id})");

        IUser selectedUser = AnsiConsole.Prompt(userSelector);

        long accountId = AnsiConsole.Ask<long>("Enter account ID:");
        string accountName = AnsiConsole.Ask<string>("Enter account name:");
        int pinCode = AnsiConsole.Ask<int>("Enter a 4-digit PIN code:");

        var account = new Account(accountId, accountName, 0, pinCode, selectedUser.Id);

        try
        {
            _accountService.AddAccountAsync(account).GetAwaiter().GetResult();
            AnsiConsole.MarkupLine($"[green]Account '{accountName}' successfully created for user {selectedUser.Name}![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]An error occurred: {ex.Message}[/]");
        }
    }
}
