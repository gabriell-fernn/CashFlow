using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Delete
{
    public class DeleteExpenseUseCase : IDeleteExpensesUseCase
    {
        private readonly IExpensesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteExpenseUseCase(IExpensesRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id)
        {
            var result = await _repository.Delete(id);

            if (result == false)
            {
                throw new ExpenseNotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }

            await _unitOfWork.Commit();
        }
    }
}
