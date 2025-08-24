using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Users
{
    public interface IUserWriteOnlyRepository
    {
        Task AddAsync(User user);
    }
}
