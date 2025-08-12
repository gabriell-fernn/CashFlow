using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpensesUseCase : IRegisterExpensesUseCase
    {
        private readonly IExpensesRepository _expensesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterExpensesUseCase(IExpensesRepository expensesRepository, IUnitOfWork unitOfWork)
        {
            _expensesRepository = expensesRepository;
            _unitOfWork = unitOfWork;
        }

        public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
        {
            Validate(request);

            var entity = new Expense
            {
                Title = request.Title,
                Description = request.Description,
                Amount = request.Amount,
                Date = request.Date,
                PaymentType = (PaymentType)request.PaymentType,
            };

            _expensesRepository.Add(entity);

            _unitOfWork.Commit();

            return new ResponseRegisterExpenseJson();
        }

        #region Private Methods

        private void Validate(RequestRegisterExpenseJson request)
        {
            var validator = new RegisterExpenseValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }

        #endregion
    }
}
