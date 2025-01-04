namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;

public abstract record ResultType
{
    private ResultType() { }

    public sealed record Success : ResultType;

    public record Failure : ResultType
    {
        public record LoginFailure : Failure
        {
            public sealed record WrongPassword : LoginFailure;

            public sealed record AccountNotFound : LoginFailure;

            public sealed record UserNotFound : LoginFailure;

            public sealed record NotAdmin : LoginFailure;

            public sealed record InvalidPinCode : LoginFailure;
        }

        public record CreateAccountFailure : Failure
        {
            public sealed record AccountAlreadyExists : CreateAccountFailure;

            public sealed record UserNotFound : CreateAccountFailure;
        }

        public record CreateUserFailure : Failure
        {
            public sealed record UserAlreadyExists : CreateUserFailure;
        }

        public record DepositMoneyFailure : Failure
        {
            public sealed record AccountNotFound : DepositMoneyFailure;

            public sealed record NegativeAmount : DepositMoneyFailure;
        }

        public record GetBalanceFailure : Failure
        {
            public sealed record AccountNotFound : GetBalanceFailure;
        }

        public record WithdrawMoneyFailure : Failure
        {
            public sealed record AccountNotFound : WithdrawMoneyFailure;

            public sealed record NegativeAmount : WithdrawMoneyFailure;

            public sealed record InsufficientFunds : WithdrawMoneyFailure;
        }

        public record GetTransactionHistoryFailure : Failure
        {
            public sealed record AccountNotFound : GetTransactionHistoryFailure;

            public sealed record NoTransactions : GetTransactionHistoryFailure;
        }
    }
}