using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios;

public class CreateAccountScenarioProvider : IScenarioProvider
{
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;

    public CreateAccountScenarioProvider(IUserRepository userRepository, IAccountRepository accountRepository)
    {
        _userRepository = userRepository;
        _accountRepository = accountRepository;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        Console.WriteLine("Running TryGetScenario in CreateAccountScenarioProvider...");
        IEnumerable<Domain.Models.IUser>? users = _userRepository.GetAllUsersAsync().GetAwaiter().GetResult();
        IEnumerable<IUser> enumerable = users as IUser[] ?? users.ToArray();
        if (users == null)
        {
            Console.WriteLine("No users found (null).");
        }
        else
        {
            Console.WriteLine($"Users found: {enumerable.Count()}");
        }

        if (users == null || !enumerable.Any())
        {
            Console.WriteLine("No valid users for scenario.");
            scenario = null;
            return false;
        }

        scenario = new CreateAccountScenario(_accountRepository, _userRepository);
        Console.WriteLine("Scenario created successfully.");
        return true;
    }
}