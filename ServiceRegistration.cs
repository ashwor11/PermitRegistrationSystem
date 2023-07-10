using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;
using PermitRegistrationSystem.Repositories;
using PermitRegistrationSystem.Repositories.Abstract;

namespace PermitRegistrationSystem;

public static class ServiceRegistration
{
    
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDbConnection>(provider =>
        {

            var connectionString = configuration.GetConnectionString("Default");
            return new MySqlConnection(connectionString);
        });

        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;



    }
}