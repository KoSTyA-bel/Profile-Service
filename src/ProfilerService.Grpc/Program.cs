using Microsoft.EntityFrameworkCore;
using ProfilerService.BLL.Interfaces;
using ProfilerService.BLL.Services;
using ProfilerService.DLL.Contexts;
using ProfilerService.DLL.Repositories;
using ProfilerService.DLL.Providers;
using ProfilerService.Grpc;
using ProfilerService.BLL.Settings;
using Microsoft.Extensions.Options;
using ProfilerService.BLL.Verifiers;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("Data");

builder.Services.AddDbContextPool<ProfileContext>(options => options.UseNpgsql(connectionString));

builder.Services.Configure<WaxWalletVerifierSettings>(builder.Configuration.GetSection(nameof(WaxWalletVerifierSettings)));
builder.Services.AddSingleton<WaxWalletVerifierSettings>(sp => sp.GetRequiredService<IOptions<WaxWalletVerifierSettings>>().Value);

builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileProvider, ProfileProvider>();
builder.Services.AddScoped<IWaxWalletVerifier, WaxWalletVerifier>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IWaxWalletVerifyService, WaxWalletVerifyService>();
builder.Services.AddScoped<IDataContext, ProfileDataContext>();

builder.Services.AddScoped(provider =>
{
    var service = provider.GetService(typeof(ProfileContext)) as ProfileContext;
    return service.Profiles;
});

builder.Services.AddGrpc();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.MapGrpcService<ProfilerService.Grpc.Services.ProfilerService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
