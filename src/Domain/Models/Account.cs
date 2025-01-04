namespace Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

public class Account
{
    public long Id { get; }

    public string Name { get; }

    public int Balance { get; private set; }

    public int PinCode { get; set; }

    public long UserId { get; set; }

    public Account(long id, string name, int balance, int pinCode, long userId)
    {
        Name = name;
        Id = id;
        Balance = balance;
        PinCode = pinCode;
        UserId = userId;
    }

    public bool DepositMoney(int amount)
    {
        if (amount <= 0)
        {
            return false;
        }

        Balance += amount;
        return true;
    }

    public bool WithdrawMoney(int amount)
    {
        if (amount <= 0)
        {
            return false;
        }

        if (Balance < amount)
        {
            return false;
        }

        Balance -= amount;
        return true;
    }
}