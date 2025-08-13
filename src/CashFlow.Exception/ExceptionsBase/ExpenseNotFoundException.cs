namespace CashFlow.Exception.ExceptionsBase
{
    public class ExpenseNotFoundException : CashFlowException
    {
        public ExpenseNotFoundException(string message) : base(message)
        {
        }
    }
}
