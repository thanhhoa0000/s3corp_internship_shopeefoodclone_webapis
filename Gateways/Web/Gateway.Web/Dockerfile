# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

RUN mkdir -p /home/app/.aspnet/https

COPY ./certs/.aspnet/https/Gateway.Web.pfx /home/app/.aspnet/https/Gateway.Web.pfx

RUN chown -R app:app /home/app/.aspnet/https && update-ca-certificates

USER app
WORKDIR /app
COPY ["nlog.config", "."]
COPY ["jwt_properties.json", "."]
EXPOSE 5000
EXPOSE 5001


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["Gateways/Web/Gateway.Web/Gateway.Web.csproj", "Gateways/Web/Gateway.Web/"]
RUN dotnet restore "./Gateways/Web/Gateway.Web/Gateway.Web.csproj"
COPY . .
WORKDIR "/src/Gateways/Web/Gateway.Web"
RUN dotnet build "./Gateway.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Gateway.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.Web.dll"]