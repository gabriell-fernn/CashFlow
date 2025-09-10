using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    internal class UsersRepository : IUsersReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
    {
        private readonly CashFlowDbContext _dbContext;

        public UsersRepository(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task DeleteAsync(User user)
        {
            var userToRemove = await _dbContext.Users.FindAsync(user.Id);
            _dbContext.Users.Remove(userToRemove!);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email.Equals(email));
        }

        public async Task<User> GetById(long id)
        {
            return await _dbContext.Users.FirstAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public void Update(User user)
        {
            _dbContext.Users.Update(user);
        }
    }
}
