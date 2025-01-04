using Itmo.ObjectOrientedProgramming.Lab5.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Services.Implementatios.AtmUsers;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Services.Implementatios.User;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<ICreateAccount, CreateAccount>();
        collection.AddScoped<ICreateUser, CreateUser>();
        collection.AddScoped<IDepositMoney, DepositMoney>();
        collection.AddScoped<IGetBalance, GetBalance>();
        collection.AddScoped<IGetTransactionHistory, GetTransactionHistory>();
        collection.AddScoped<ILoginUser, LoginUser>();
        collection.AddScoped<ILoginAdmin, LoginAdmin>();
        collection.AddScoped<IWithdrawMoney, WithdrawMoney>();

        return collection;
    }
}