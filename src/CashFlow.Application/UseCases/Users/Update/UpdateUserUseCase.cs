using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.Update
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUserUpdateOnlyRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggedUser _loggedUser;
        private readonly IUsersReadOnlyRepository _usersReadOnlyRepository;

        public UpdateUserUseCase(
            IUserUpdateOnlyRepository userRepository,
            IUnitOfWork unitOfWork,
            ILoggedUser loggedUser,
            IUsersReadOnlyRepository usersReadOnlyRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _loggedUser = loggedUser;
            _usersReadOnlyRepository = usersReadOnlyRepository;
        }

        public async Task Execute(RequestUpdateUserJson request)
        {
            var loggedUser = await _loggedUser.Get();

            await Validate(request, loggedUser.Email);

            var user = await _userRepository.GetById(loggedUser.Id);

            user.Name = request.Name;
            user.Email = request.Email;

            _userRepository.Update(user);

            await _unitOfWork.Commit();
        }

        private async Task Validate(RequestUpdateUserJson request, string currentEmail)
        {
            var validator = new UpdateUserValidator();

            var result = validator.Validate(request);

            if (currentEmail.Equals(request.Email) == false)
            {
                var userExists = await _usersReadOnlyRepository.ExistsByEmailAsync(request.Email);

                if (userExists)
                {
                    result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
                }
            }

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
