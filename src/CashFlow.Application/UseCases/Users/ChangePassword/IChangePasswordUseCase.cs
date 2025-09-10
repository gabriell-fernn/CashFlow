using CashFlow.Communication.Requests;

namespace CashFlow.Domain.Repositories.Users
{
    public interface IChangePasswordUseCase
    {
        Task Execute(RequestChangePasswordJson request);
    }
}
