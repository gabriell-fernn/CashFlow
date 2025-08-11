using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories
{
    public interface IExpensesRepository
    {
        void Add(Expense expense);
    }
}
