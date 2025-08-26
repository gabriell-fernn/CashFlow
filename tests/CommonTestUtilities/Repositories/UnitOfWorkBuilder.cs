using CashFlow.Domain.Repositories;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UnitOfWorkBuilder
    {
        public static IUnitOfWork Build()
        {
            var unitOfWork = new Mock<IUnitOfWork>();

            return unitOfWork.Object;
        }
    }
}
