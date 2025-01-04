using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios;

public class CreateUserScenarioProvider : IScenarioProvider
{
    private readonly IUserRepository _userRepository;

    public CreateUserScenarioProvider(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        scenario = new CreateUserScenario(_userRepository);
        return true;
    }
}