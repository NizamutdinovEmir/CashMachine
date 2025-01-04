using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public interface ILoginAdmin
{
    public Task<ResultType> AuthorizationAdmin(string name, string password);
}