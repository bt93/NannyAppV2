using NannyAPI.Security;
using NannyData.Interfaces;
using NannyData.SQLDAL;

namespace NannyAPI.DependencyInjection
{
    public static class RepositoryServiceCollectionExtensions
    {
        /// <summary>
        /// Injects the needed services
        /// </summary>
        /// <param name="services">The services</param>
        /// <param name="configuration">The configuration</param>
        /// <returns>The services</returns>
        public static IServiceCollection AddRepositories(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("NannyDB");
            string jwtSecret = configuration["jwtSecret"];
            int workFactor = int.Parse(configuration["HashSettings:WorkFactor"]);
            int salt = int.Parse(configuration["HashSettings:Salt"]);
            int keyBytes = int.Parse(configuration["HashSettings:KeyBytes"]);

            return services
                .AddSingleton<ITokenGenerator, JWTGenerator>(s => new JWTGenerator(jwtSecret, new SQLRefreshTokenDAO(connectionString)))
                .AddSingleton<IPasswordHasher, PasswordHasher>(s => new PasswordHasher(workFactor, salt, keyBytes))
                .AddSingleton<IUserDAO>(s => new SQLUserDAO(connectionString))
                .AddSingleton<IAddressDAO>(s => new SQLAddressDAO(connectionString))
                .AddSingleton<IChildDAO>(s => new SQLChildDAO(connectionString))
                .AddSingleton<IRoleDAO>(s => new SQLRoleDAO(connectionString))
                .AddSingleton<ISessionDAO>(s => new SQLSessionDAO(connectionString));
        }
    }
}

