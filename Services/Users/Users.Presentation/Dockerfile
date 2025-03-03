# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

RUN mkdir -p /home/app/.aspnet/https

COPY ./certs/.aspnet/https/Users.API.pfx /home/app/.aspnet/https/Users.API.pfx

RUN chown -R app:app /home/app/.aspnet/https

USER app
WORKDIR /app
COPY ["jwt_properties.json", "."]
COPY ["nlog.config", "."]
EXPOSE 5020
EXPOSE 5021


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["Services/Users/Users.Presentation/Users.Presentation.csproj", "Services/Users/Users.Presentation/"]
COPY ["Services/Users/Users.Infrastructure/Users.Infrastructure.csproj", "Services/Users/Users.Infrastructure/"]
COPY ["Services/Users/Users.Application/Users.Application.csproj", "Services/Users/Users.Application/"]
COPY ["Services/Users/Users.Domain/Users.Domain.csproj", "Services/Users/Users.Domain/"]
RUN dotnet restore "./Services/Users/Users.Presentation/Users.Presentation.csproj"
COPY . .
WORKDIR "/src/Services/Users/Users.Presentation"
RUN dotnet build "./Users.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Users.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.Presentation.dll"]