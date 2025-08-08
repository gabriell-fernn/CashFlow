using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
    {
        public RegisterExpenseValidator()
        {
            RuleFor(expense => expense.Title)
                .NotEmpty().WithMessage("Title is required.");
            RuleFor(expense => expense.Date)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Date is invalid.");
            RuleFor(expense => expense.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
            RuleFor(expense => expense.PaymentType)
                .IsInEnum().WithMessage("Invalid payment type.");
        }
    }
}
