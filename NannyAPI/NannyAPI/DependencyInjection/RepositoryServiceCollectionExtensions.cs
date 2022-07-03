using NannyAPI.Security;
using NannyData.Interfaces;
using NannyData.SQLDAL;

namespace NannyAPI.DependencyInjection
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services, 
            IConfiguration configuration,
            string jwtSecret)
        {
            string? connectionString = configuration.GetConnectionString("NannyDB");

            return services
                .AddSingleton<ITokenGenerator, JWTGenerator>(s => new JWTGenerator(jwtSecret))
                .AddSingleton<IPasswordHasher, PasswordHasher>(s => new PasswordHasher())
                .AddSingleton<IUserDAO>(s => new SQLUserDAO(connectionString))
                .AddSingleton<IAddressDAO>(s => new SQLAddressDAO(connectionString));
        }
    }
}
