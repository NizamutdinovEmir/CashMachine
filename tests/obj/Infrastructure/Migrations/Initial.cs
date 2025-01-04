using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        """
        create type user_role as enum
        (
            'user',
            'admin'
        );

        create type type_of_transaction as enum
        (
           'deposit',
           'withdrawal',
           'get_balance',
           'get_transaction_history'
        );

        create table users
        (
            user_id bigint primary key,
            user_role user_role not null,
            user_name text not null,
            user_password text not null
        );
        
        create table accounts_data
        (
            account_id bigint primary key,
            account_name text not null,
            account_balance int not null,
            account_pin int not null,
            user_id bigint not null references users(user_id) on delete cascade
        );

        create table transactions_info
        (
            transaction_id bigint primary key,
            transaction_account bigint references accounts_data(account_id) on delete set null,
            transaction_type type_of_transaction not null,
            transaction_user_id bigint not null references users(user_id) on delete cascade
        );
        """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
        drop table users;
        drop table accounts;
        drop table accounts_data;
        drop table transactions_info;
        """;
}
