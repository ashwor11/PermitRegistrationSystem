using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;
using Application;
using Core;
using MediatR;
using MySql.Data.MySqlClient;
using Persistence;


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


        services.AddApplicationServices();
        services.AddPersistenceServices(configuration);
        services.AddCoreServices(configuration);
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;



    }
}