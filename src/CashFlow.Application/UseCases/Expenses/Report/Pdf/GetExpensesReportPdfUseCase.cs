
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Report.Pdf
{
    public class GetExpensesReportPdfUseCase : IGetExpensesReportPdfUseCase
    {
        private const string CURRENT_SYMBOL = "R$";
        private readonly IExpensesReadOnlyRepository _repository;

        public GetExpensesReportPdfUseCase(IExpensesReadOnlyRepository repository)
        {
            _repository = repository;
        }

        public async Task<byte[]> Execute(DateOnly month)
        {
            var results = await _repository.GetByMonth(month);

            if (results.Count == 0)
            {
                return [];
            }

            return [];
        }
    }
}
