using CashFlow.Application.UseCases.Users.Login;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;

namespace UseCases.Test.Login.DoLogin
{
    public class DoLoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestLoginJsonBuilder.Build();
            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Token.Should().NotBeNullOrWithSpace();
        }

        [Fact]
        public async Task Error_User_Not_Found()
        {

        }

        [Fact]
        public async Task Error_Password_Not_Match()
        {

        }

        private DoLoginUseCase CreateUseCase()
        {
            var passwordEncrypter = PasswordEncripterBuilder.Build();
            var tokenGenerator = JwtTokenGeneratorBuilder.Build();
            var readRepository = new UserReadOnlyRepositoryBuilder().Build();

            return new DoLoginUseCase(readRepository, passwordEncrypter, tokenGenerator);
        }
    }
}
