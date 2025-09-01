using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using FluentAssertions;

namespace UseCases.Test.Users.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
            result.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var useCase = CreateUseCase();

            var action = async () => await useCase.Execute(request);

            var result = await action.Should().ThrowAsync<ErrorOnValidationException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NAME_EMTY));
        }

        [Fact]
        public async Task Error_Email_Exists()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase(request.Email);

            var action = async () => await useCase.Execute(request);

            var result = await action.Should().ThrowAsync<ErrorOnValidationException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
        }

        private RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
            var tokenGenerator = JwtTokenGeneratorBuilder.Build();
            var passwordEncripter = new PasswordEncripterBuilder().Build();
            var readRepository = new UserReadOnlyRepositoryBuilder();

            if(string.IsNullOrWhiteSpace(email) == false)
            {
                readRepository.ExistsByEmailAsync(email);
            }

            return new RegisterUserUseCase(mapper, passwordEncripter, readRepository.Build(), writeRepository, unitOfWork, tokenGenerator);
        }
    }
}
