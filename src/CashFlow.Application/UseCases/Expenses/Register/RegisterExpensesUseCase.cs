using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpensesUseCase
    {
        public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
        {
            Validate(request);

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
