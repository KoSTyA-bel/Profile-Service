using Microsoft.EntityFrameworkCore;
using ProfilerService.Grpc;
using ProfilerService.BLL.Settings;
using ProfilerService.DLL;
using ProfilerService.BLL;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("Data");
var apiUrl = Environment.GetEnvironmentVariable("API_URL");
var collectionName = Environment.GetEnvironmentVariable("COLLECTION_NAME");

builder.Services.Configure<WaxWalletVerifierSettings>(builder.Configuration.GetSection(nameof(WaxWalletVerifierSettings)));

builder.Services.AddProfileService();
builder.Services.AddProfileDataBase(connectionString);
builder.Services.AddWaxWalletVerifier();    

builder.Services.AddGrpc();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.MapGrpcService<ProfilerService.Grpc.Services.ProfilerService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

var settings = app.Services.GetService(typeof(WaxWalletVerifierSettings)) as WaxWalletVerifierSettings;

if (apiUrl is not null)
{
    settings.ApiUrl = apiUrl;
}

if (collectionName is not null)
{
    settings.CollectionName = collectionName;
}

app.Run();
