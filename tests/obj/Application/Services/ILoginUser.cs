using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public interface ILoginUser
{
    public Task<ResultType> AuthorizationUser(long id, int pinCode);
}