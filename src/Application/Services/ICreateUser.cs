using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Models;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public interface ICreateUser
{
    public Task<ResultType> ExecuteAsync(long id, UserRole role, string userName, string password);
}