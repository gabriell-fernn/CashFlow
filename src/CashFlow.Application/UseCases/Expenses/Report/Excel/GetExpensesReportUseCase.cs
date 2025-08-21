
using CashFlow.Domain.Enums;
using CashFlow.Domain.PaymentTypes;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Report.Excel
{
    public class GetExpensesReportUseCase : IGetExpensesReportUseCase
    {
        private const string CURRENT_SYMBOL = "R$";
        private readonly IExpensesReadOnlyRepository _repository;

        public GetExpensesReportUseCase(IExpensesReadOnlyRepository repository)
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

            using var workbook = new XLWorkbook();

            workbook.Author = "CashFlow";
            workbook.Style.Font.FontSize = 12;
            workbook.Style.Font.FontName = "Times New Roman";


            var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

            InsertHeader(worksheet);

            var row = 2;
            foreach (var expense in results)
            {
                worksheet.Cell($"A{row}").Value = expense.Title;
                worksheet.Cell($"B{row}").Value = expense.Date;
                worksheet.Cell($"C{row}").Value = ConvertPaymentType(expense.PaymentType);

                worksheet.Cell($"D{row}").Value = expense.Amount;
                worksheet.Cell($"D{row}").Style.NumberFormat.Format = $"-{CURRENT_SYMBOL} #,##0.00";

                worksheet.Cell($"E{row}").Value = expense.Description;

                row++;
            }

            worksheet.Columns().AdjustToContents();

            var file = new MemoryStream();
            workbook.SaveAs(file);

            return file.ToArray();
        }

        #region Private Methods

        private string ConvertPaymentType(PaymentType payment)
        {
            return payment switch
            {
                PaymentType.Cash => ResourcePaymentTypeMessages.Cash,
                PaymentType.CreditCard => ResourcePaymentTypeMessages.CreditCard,
                PaymentType.DebitCard => ResourcePaymentTypeMessages.DebitCard,
                PaymentType.Pix => ResourcePaymentTypeMessages.Pix,
                _ => string.Empty,
            };
        }

        private void InsertHeader(IXLWorksheet worksheet)
        {
            worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
            worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
            worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
            worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
            worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;

            worksheet.Cells("A1:E1").Style.Font.Bold = true;

            worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6");

            worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

        }

        #endregion
    }
}
