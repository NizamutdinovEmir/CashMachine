using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation;

public class ScenarioRunner
{
    private readonly IEnumerable<IScenarioProvider> _providers;

    public ScenarioRunner(IEnumerable<IScenarioProvider> providers)
    {
        _providers = providers;
    }

    public void Run()
    {
        IEnumerable<IScenario> scenarios = GetScenarios();

        Console.WriteLine("ТУТ1");

        if (!scenarios.Any())
        {
            AnsiConsole.MarkupLine("[red]No scenarios available to run.[/]");
            return;
        }

        SelectionPrompt<IScenario> selector = new SelectionPrompt<IScenario>()
            .Title("Select action")
            .AddChoices(scenarios)
            .UseConverter(x => x.Name);

        IScenario scenario = AnsiConsole.Prompt(selector);
        scenario.Run();
    }

    private IEnumerable<IScenario> GetScenarios()
    {
        foreach (IScenarioProvider provider in _providers)
        {
            Console.WriteLine($"Provider: {provider.GetType().Name}");
            if (provider.TryGetScenario(out IScenario? scenario))
                yield return scenario;
        }
    }
}