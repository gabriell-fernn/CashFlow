using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Delete
{
    public class DeleteExpenseUseCase : IDeleteExpensesUseCase
    {
        private readonly IExpensesWriteOnlyRepository _repository;
        private readonly IExpensesReadOnlyRepository _readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggedUser _loggedUser;

        public DeleteExpenseUseCase(IExpensesWriteOnlyRepository repository, IUnitOfWork unitOfWork, ILoggedUser loggedUser, IExpensesReadOnlyRepository readOnlyRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _loggedUser = loggedUser;
            _readOnlyRepository = readOnlyRepository;
        }

        public async Task Execute(long id)
        {
            var loggedUser = await _loggedUser.Get();

            var expense = await _readOnlyRepository.GetById(loggedUser, id);

            if (expense is null)
            {
                throw new ExpenseNotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }

            await _repository.Delete(id);

            await _unitOfWork.Commit();
        }
    }
}
