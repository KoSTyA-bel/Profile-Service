using Microsoft.EntityFrameworkCore;
using ProfileService.BLL.Settings;
using FluentValidation;
using Service.Grpc;
using ProfileService.BLL;
using ProfileService.DLL;
using ProfileService.Grpc;
using ProfileService.Grpc.Interceptors;
using ProfileService.Grpc.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("Data");

builder.AddNFTVerifierSettings();
builder.AddAppSettings();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddProfileService();
builder.Services.AddProfileDataBase(connectionString);
builder.Services.AddWaxWalletVerifier();

builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<ErrorHandlingInterceptor>();
    options.Interceptors.Add<ValidationInterceptor>();
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.MapGrpcService<ProfileService.Grpc.Services.ProfilerService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.Run();
