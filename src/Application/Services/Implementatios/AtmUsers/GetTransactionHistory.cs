using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services.Implementatios.AtmUsers;

public class GetTransactionHistory : IGetTransactionHistory
{
    private readonly IAccountTransactionRepository _repository;
    private readonly IDisplay _display;

    public GetTransactionHistory(IAccountTransactionRepository repository, IDisplay display)
    {
        _repository = repository;
        _display = display;
    }

    public async Task<ResultType> ExecuteAsyncHistory(long accountId)
    {
        IList<Domain.Models.Transaction> transaction = await _repository.GetTransactionsByUserId(accountId).ConfigureAwait(false);
        if (transaction == null || transaction.Count == 0)
            return new ResultType.Failure.GetTransactionHistoryFailure.NoTransactions();

        transaction.ToList().ForEach(x => _display.Display($"Account {x.AccountId}, id {x.Id}, type {x.Type}"));
        return new ResultType.Success();
    }
}