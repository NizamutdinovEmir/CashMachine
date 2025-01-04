namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services.Implementatios;

public class DisplayText : IDisplay
{
    public void Display(string message)
    {
        Console.WriteLine(message);
    }
}