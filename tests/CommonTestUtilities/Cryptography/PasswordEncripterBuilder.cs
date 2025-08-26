using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography
{
    public class PasswordEncripterBuilder
    {
        public static IPasswordEncripter Build()
        {
            var mock = new Mock<IPasswordEncripter>();

            mock.Setup(x => x.Encrypt(It.IsAny<string>())).Returns("!@ashakm#42121_");

            return mock.Object;
        }
    }
}
