using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public interface IGetTransactionHistory
{
    public Task<ResultType> ExecuteAsyncHistory(long accountId);
}