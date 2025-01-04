using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Services.Implementatios.AtmUsers;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;
using Moq;
using Xunit;

namespace Lab5.Tests;

public class TestScenario
{
    [Fact]
    public async Task WithdrawMoneyShouldBeSuccess()
    {
        // Arrange
        var user = new AtmUser(1, UserRole.User, "name", "password");
        var account = new Account(1, "account", 300, 1234, user.Id);
        var mockRepository = new Mock<IAccountRepository>();
        int amount = 100;
        mockRepository.Setup(r => r.GetAccountByIdAsync(account.Id)).Returns(Task.FromResult<Account?>(account));
        var withdrawMoney = new WithdrawMoney(mockRepository.Object);

        // Act
        ResultType resultType = await withdrawMoney.ExecuteAsyncWithdraw(account.Id, amount);

        // Assert
        Assert.Equal(200, account.Balance);
        Assert.IsType<ResultType.Success>(resultType);
        mockRepository.Verify(r => r.UpdateAccountMoneyAsync(account, It.Is<int>(a => a == -amount)), Times.Once);
    }

    [Fact]
    public async Task UpdateAccountMoneyAsync()
    {
        // Arrange
        var user = new AtmUser(1, UserRole.User, "name", "password");
        var account = new Account(1, "account", 300, 1234, user.Id);
        var mockRepository = new Mock<IAccountRepository>();
        int amount = 100;
        mockRepository.Setup(r => r.GetAccountByIdAsync(account.Id)).Returns(Task.FromResult<Account?>(account));
        var depositMoney = new DepositMoney(mockRepository.Object);

        // Act
        ResultType resultType = await depositMoney.ExecuteAsyncDeposite(account.Id, amount);

        // Assert
        Assert.Equal(400, account.Balance);
        Assert.IsType<ResultType.Success>(resultType);
        mockRepository.Verify(r => r.UpdateAccountMoneyAsync(account, It.Is<int>(a => a == amount)), Times.Once);
    }
}