namespace CashFlow.Application.UseCases.Expenses.Report.Excel
{
    public interface IGetExpensesReportUseCase
    {
        Task<byte[]> Execute(DateOnly month);
    }
}
