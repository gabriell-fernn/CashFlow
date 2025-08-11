using CashFlow.Application.UseCases.Expenses.Register;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection service)
        {
            service.AddScoped<IRegisterExpensesUseCase, RegisterExpensesUseCase>();
        }
    }
}
