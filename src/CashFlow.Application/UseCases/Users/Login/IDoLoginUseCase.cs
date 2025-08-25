using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

namespace CashFlow.Application.UseCases.Users.Login
{
    public interface IDoLoginUseCase
    {
        Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
    }
}
