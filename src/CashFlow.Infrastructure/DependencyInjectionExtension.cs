using CashFlow.Domain.Repositories;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection service)
        {
            service.AddScoped<IExpensesRepository, ExpensesRepository>();
        }
    }
}
