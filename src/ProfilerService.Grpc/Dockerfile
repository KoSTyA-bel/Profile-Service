#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ProfilerService.Grpc/ProfilerService.Grpc.csproj", "src/ProfilerService.Grpc/"]
COPY ["src/ProfilerService.BLL/ProfilerService.BLL.csproj", "src/ProfilerService.BLL/"]
COPY ["src/ProfilerService.DLL/ProfilerService.DLL.csproj", "src/ProfilerService.DLL/"]
RUN dotnet restore "src/ProfilerService.Grpc/ProfilerService.Grpc.csproj"
COPY . .
WORKDIR "/src/src/ProfilerService.Grpc"
RUN dotnet build "ProfilerService.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProfilerService.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProfilerService.Grpc.dll"]