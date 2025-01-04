using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, CreateUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CreateAccountScenarioProvider>();

        return collection;
    }
}