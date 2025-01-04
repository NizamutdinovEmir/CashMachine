using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;

public interface IAccountTransactionRepository
{
    Task CreateTransactionAsync(Transaction transaction);

    Task<IList<Transaction>> GetTransactionsByUserId(long id);
}