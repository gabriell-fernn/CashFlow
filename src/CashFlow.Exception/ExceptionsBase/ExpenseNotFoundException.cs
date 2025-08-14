using System.Net;

namespace CashFlow.Exception.ExceptionsBase
{
    public class ExpenseNotFoundException : CashFlowException
    {
        public ExpenseNotFoundException(string message) : base(message)
        {
        }

        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
