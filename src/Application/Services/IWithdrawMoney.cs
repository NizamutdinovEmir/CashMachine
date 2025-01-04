using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public interface IWithdrawMoney
{
    public Task<ResultType> ExecuteAsyncWithdraw(long accountId, int amount);
}