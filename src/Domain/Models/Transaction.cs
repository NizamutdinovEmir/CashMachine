namespace Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

public class Transaction
{
    public long Id { get; set; }

    public long AccountId { get; set; }

    public long UserId { get; set; }

    public TypeOfTransaction Type { get; set; }
}