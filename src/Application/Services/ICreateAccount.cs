using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public interface ICreateAccount
{
    public Task<ResultType> ExecuteAsyncCreateAccount(long id, long userid, string name, int pinCode);
}