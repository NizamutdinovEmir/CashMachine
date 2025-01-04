using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Repositories;

public class DataBaseAccountTransactionRepository : IAccountTransactionRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public DataBaseAccountTransactionRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task CreateTransactionAsync(Transaction transaction)
    {
        const string sql = """
                           insert into transactions_info (transaction_id, transaction_account, transaction_type, transaction_user_id)
                           values (:id, :accountId, :type, :userId);
                           """;

        using NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default)
            .ConfigureAwait(false);

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", transaction.Id)
            .AddParameter("accountId", transaction.AccountId)
            .AddParameter("type", transaction.Type)
            .AddParameter("userId", transaction.UserId);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }

    public async Task<IList<Transaction>> GetTransactionsByUserId(long id)
    {
        const string sql = """
                           select transaction_id, transaction_account, transaction_type, transaction_user_id
                           from transactions_info
                           where transaction_user_id = :userId;
                           """;

        using NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default)
            .ConfigureAwait(false);

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("userId", id);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        var transactions = new List<Transaction>();
        while (await reader.ReadAsync().ConfigureAwait(false))
        {
            transactions.Add(new Transaction
            {
                Id = reader.GetInt64(0),
                AccountId = reader.IsDBNull(1) ? 0 : reader.GetInt64(1),
                Type = reader.GetFieldValue<TypeOfTransaction>(2),
                UserId = reader.GetInt64(3),
            });
        }

        return transactions;
    }
}