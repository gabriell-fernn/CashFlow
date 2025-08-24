namespace CashFlow.Domain.Repositories.Users
{
    public interface IUsersReadOnlyRepository
    {
        Task<bool> ExistsByEmailAsync(string email);
    }
}
