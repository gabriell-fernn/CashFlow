namespace CashFlow.Application.UseCases.Expenses.Report.Pdf
{
    public interface IGetExpensesReportPdfUseCase
    {
        Task<byte[]> Execute(DateOnly month);
    }
}
