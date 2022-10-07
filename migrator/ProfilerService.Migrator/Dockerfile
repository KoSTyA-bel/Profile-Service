#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["migrator/ProfilerService.Migrator/ProfileService.Migrator.csproj", "migrator/ProfilerService.Migrator/"]
COPY ["src/ProfilerService.DLL/ProfileService.DLL.csproj", "src/ProfilerService.DLL/"]
COPY ["src/ProfilerService.BLL/ProfileService.BLL.csproj", "src/ProfilerService.BLL/"]
RUN dotnet restore "migrator/ProfilerService.Migrator/ProfileService.Migrator.csproj"
COPY . .
WORKDIR "/src/migrator/ProfilerService.Migrator"
RUN dotnet build "ProfileService.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProfileService.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProfileService.Migrator.dll", "--connection-string-source", "env", "--connection-string-env-variable-name", "CONNECTION_STRING"]