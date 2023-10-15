using Microsoft.EntityFrameworkCore;
using WalletApp.Application.Interfaces;
using WalletApp.Application.Mapping;
using WalletApp.Application.Services;
using WalletApp.Domain;
using WalletApp.Domain.DataBase;
using WalletApp.Domain.Repositories.Implementation;
using WalletApp.Domain.Repositories.Interface;


namespace WalletApp.Api.DI
{   
    public static class DbContextExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DerfaultConnection");

            services.AddDbContext<ApplicationsContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly("WalletApp.Domain")));

            return services;
        }
    }

    public static partial class DependencyEntityRepositoryConfigurator
    {
        public static void AddEntityRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }

        public static void AddGenericRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IDbRepository<>), typeof(DbRepository<>));
        }

        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUow, Uou>();
        }

        public static void AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingConfigurator));
        }
    }

    public static partial class DependencyСonfigurator
    {
        public static void AddEntityServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
