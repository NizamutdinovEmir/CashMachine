using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public interface IGetBalance
{
    public Task<ResultType> ExecuteAsyncBalance(long accountId);
}