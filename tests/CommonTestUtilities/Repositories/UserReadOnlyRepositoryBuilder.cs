using CashFlow.Domain.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private readonly Mock<IUsersReadOnlyRepository> _repository;

        public UserReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IUsersReadOnlyRepository>();
        }

        public void ExistsByEmailAsync(string email)
        {
            _repository.Setup(userReadOnly => userReadOnly.ExistsByEmailAsync(email)).ReturnsAsync(true);
        }

        public IUsersReadOnlyRepository Build() => _repository.Object;
    }
}
