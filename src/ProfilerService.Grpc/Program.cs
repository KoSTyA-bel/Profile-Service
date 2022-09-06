using Microsoft.EntityFrameworkCore;
using ProfilerService.BLL.Interfaces;
using ProfilerService.BLL.Services;
using ProfilerService.DLL.Contexts;
using ProfilerService.DLL.Repositories;
using ProfilerService.Grpc;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("Data");

builder.Services.AddDbContextPool<ProfileContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IDataContext, ProfileDataContext>();

builder.Services.AddGrpc();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.MapGrpcService<ProfilerService.Grpc.Services.ProfilerService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
