using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;
using MyRecipeBook.Infrastructure.Extensions;

namespace MyRecipeBook.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddFluenteMigrator(services, configuration);
        AddRepositories(services);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));

        services.AddDbContext<MyRecipeBookDbContext>(dbContextOptions =>
        {
            dbContextOptions.UseMySql(connectionString, serverVersion);
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUnitfOfWork, UnitfOfWork>();
    }

    private static void AddFluenteMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options.AddMySql5()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.Load("MyRecipeBook.Infrastructure")).For.All();
        });
    }
}