using CashFlow.Domain.Entities;

namespace CashFlow.Application.UseCases.Expenses.Delete
{
    public interface IDeleteExpensesUseCase
    {
        Task Execute(long id);    
    }
}
