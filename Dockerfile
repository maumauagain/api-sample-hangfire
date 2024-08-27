#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/nightly/aspnet:9.0-preview AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/nightly/sdk:9.0-preview AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Hangfire.API/Hangfire.API.csproj", "src/Hangfire.API/"]
RUN dotnet restore "./src/Hangfire.API/Hangfire.API.csproj"
COPY . .
WORKDIR "/src/src/Hangfire.API"
RUN dotnet build "./Hangfire.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Hangfire.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS="http://+:8080"
ENV ASPNETCORE_ENVIRONMENT="Development"
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hangfire.API.dll"]