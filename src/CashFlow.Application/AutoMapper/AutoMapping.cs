using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            ResquestToEntity();
            EntityToResponse();
        }

        private void ResquestToEntity()
        {
            CreateMap<RequestExpenseJson, Expense>();
        }   

        private void EntityToResponse()
        {
            CreateMap<Expense, ResponseRegisterExpenseJson>();
            CreateMap<Expense, ResponseShortExpensesJson>();
            CreateMap<Expense, ResponseExpenseJson>();
        }
    }
}
