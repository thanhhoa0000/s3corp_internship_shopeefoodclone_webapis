# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

RUN mkdir -p /home/app/.aspnet/https

COPY ./certs/.aspnet/https/Payment.API.pfx /home/app/.aspnet/https/Payment.API.pfx

RUN chown -R app:app /home/app/.aspnet/https

USER app
WORKDIR /app
COPY ["jwt_properties.json", "."]
COPY ["nlog.config", "."]
EXPOSE 5070
EXPOSE 5071


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["Services/Payment/Payment.Presentation/Payment.Presentation.csproj", "Services/Payment/Payment.Presentation/"]
COPY ["Services/Payment/Payment.Infrastructure/Payment.Infrastructure.csproj", "Services/Payment/Payment.Infrastructure/"]
COPY ["Services/Payment/Payment.Application/Payment.Application.csproj", "Services/Payment/Payment.Application/"]
COPY ["Services/Payment/Payment.Domain/Payment.Domain.csproj", "Services/Payment/Payment.Domain/"]
RUN dotnet restore "./Services/Payment/Payment.Presentation/Payment.Presentation.csproj"
COPY . .
WORKDIR "/src/Services/Payment/Payment.Presentation"
RUN dotnet build "./Payment.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Payment.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.Presentation.dll"]