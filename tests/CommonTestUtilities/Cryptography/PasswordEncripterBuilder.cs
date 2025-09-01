using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography
{
    public class PasswordEncripterBuilder
    {
        private readonly Mock<IPasswordEncripter> _mock;

        public PasswordEncripterBuilder()
        {
            _mock = new Mock<IPasswordEncripter>();

            _mock.Setup(x => x.Encrypt(It.IsAny<string>())).Returns("!@ashakm#42121_");

        }
        public PasswordEncripterBuilder Verify(string? password)
        {
            if (string.IsNullOrWhiteSpace(password) == false)
            {
                _mock.Setup(p => p.Verify(password, It.IsAny<string>())).Returns(true);
            }

            return this;
        }

        public IPasswordEncripter Build() => _mock.Object;
    }
}
