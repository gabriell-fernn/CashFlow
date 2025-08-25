using CashFlow.Application.AutoMapper;
using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Report.Excel;
using CashFlow.Application.UseCases.Expenses.Report.Pdf;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Application.UseCases.Users.Login;
using CashFlow.Application.UseCases.Users.Register;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection service)
        {
            AddAutoMapper(service);
            AddUseCase(service);
        }

        private static void AddAutoMapper(IServiceCollection service)
        {
            service.AddAutoMapper(typeof(AutoMapping));
        }

        private static void AddUseCase(IServiceCollection service)
        {
            service.AddScoped<IRegisterExpensesUseCase, RegisterExpensesUseCase>();
            service.AddScoped<IGetAllExpensesUseCase, GetAllExpensesUseCase>();
            service.AddScoped<IGetByIdExpenseUseCase, GetByIdExpenseUseCase>();
            service.AddScoped<IDeleteExpensesUseCase, DeleteExpenseUseCase>();
            service.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
            service.AddScoped<IGetExpensesReportUseCase, GetExpensesReportUseCase>();
            service.AddScoped<IGetExpensesReportPdfUseCase, GetExpensesReportPdfUseCase>();
            service.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            service.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        }
    }
}