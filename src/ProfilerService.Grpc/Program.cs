using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProfileService.BLL;
using ProfileService.DLL;
using ProfileService.Grpc;
using ProfileService.Grpc.Infrastructure.Configurations;
using ProfileService.Grpc.Interceptors;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("Data");

builder.AddNFTVerifierSettings();
builder.AddAppSettings();

builder.Services
    .AddProfileService()
    .AddProfileDataBase(connectionString)
    .AddWaxWalletVerifier()
    .AddValidatorsFromAssembly(typeof(Program).Assembly)
    .AddAutoMapper(typeof(MappingProfile))
    .AddGrpc(options =>
    {
        options.Interceptors.Add<ErrorHandlingInterceptor>();
        options.Interceptors.Add<ValidationInterceptor>();
    });

var app = builder.Build();

app.MapGrpcService<ProfileService.Grpc.Services.ProfilerService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.Run();
