using System.CommandLine;
using AuctionService.DatabaseMigrator.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProfilerService.BLL.Interfaces;
using ProfilerService.DLL.Contexts;

var rootCommand = new RootCommand("Migrate database by connection string via EntityFramework");
var connectionStringSourceOption = new Option<string>("--connection-string-source",
    "Connection String Source: Available options: env, option");
var connectionStringOption = new Option<string>("--connection-string",
    "Connection string for connect to database");
var connectionStringEnvVariableName = new Option<string>("--connection-string-env-variable-name",
    "Name of env variable which contains the connection string");

rootCommand.AddOption(connectionStringSourceOption);
rootCommand.AddOption(connectionStringOption);
rootCommand.AddOption(connectionStringEnvVariableName);

rootCommand.SetHandler<string, string, string>(MigrateDatabase,
    connectionStringSourceOption,
    connectionStringOption,
    connectionStringEnvVariableName);

return await rootCommand.InvokeAsync(args);

static void MigrateDatabase(string source, string connection, string envName)
{
    source = "option";
    if (source != "env" && source != "option")
    {
        throw new InvalidDataException("Invalid value for '--connection-string-source' option");
    }

    var connectionString = source == "option"
        ? "Host=localhost;Port=5432;Database=ProfileDb;User Id=postgres;Password=123"
        : Environment.GetEnvironmentVariable(envName);

    MigratePostgreSqlServer(connectionString!);
}

static void Migrate<TContext>(Func<IServiceCollection, IServiceCollection> configure)
    where TContext : DbContext
{
    var services = new ServiceCollection()
                        .AddLogging(configure => configure.AddConsole());

    configure(services)
        .BuildServiceProvider()
        .MigrateDatabaseFromContext<TContext>();
}

static void MigratePostgreSqlServer(string connectionString)
{
    Migrate<ProfileContext>(services =>
        services.AddDbContext<ProfileContext>(options =>
        {
            options.UseNpgsql(connectionString);
        }));
}


// DataAccessServiceExtensions

// todo: dal!
//public static IServiceCollection AddPostgreSqlContext(this IServiceCollection services,
//            Action<DbContextOptionsBuilder> options)
//{
//    services.AddDbContextPool<AuctionDbContext>(options);
//    services.AddScoped<IDataContext, AuctionDataContext>();
//
//    services.AddScoped(serviceProvider =>
//        serviceProvider.GetRequiredService<AuctionDbContext>()
//            .Set<AuctionEvent>());
//    services.AddScoped(serviceProvider =>
//        serviceProvider.GetRequiredService<AuctionDbContext>()
//            .Set<BidLog>());
//
//    return services;
//}

/// AddProviders
/// AddRepositories
/// 
//public static IServiceCollection AddRepositories(this IServiceCollection services)
//{
//    services.AddScoped<IAuctionEventRepository, AuctionEventRepository>();
//    services.AddScoped<IBidLogRepository, BidLogRepository>();
//
//    return services;
//}