using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using CashFlow.Infrastructure.Extensions;
using CashFlow.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<IPasswordEncripter, Security.BCrypt>();

            AddToken(service, configuration);
            AddRepositories(service);

            if (configuration.IsTestEnvironment() == false)
            {
                AddDbContext(service, configuration);

            }
        }

        private static void AddToken(IServiceCollection service, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            service.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        }

        private static void AddRepositories(IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
            service.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>();
            service.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepository>();
            service.AddScoped<IUsersReadOnlyRepository, UsersRepository>();
            service.AddScoped<IUserWriteOnlyRepository, UsersRepository>();
        }

        private static void AddDbContext(IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");

            var serverVersion = ServerVersion.AutoDetect(connectionString);

            service.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
        }
    }
}
