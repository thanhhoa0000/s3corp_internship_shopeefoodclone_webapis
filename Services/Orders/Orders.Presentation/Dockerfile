# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

RUN mkdir -p /home/app/.aspnet/https

COPY ./certs/.aspnet/https/Orders.API.pfx /home/app/.aspnet/https/Orders.API.pfx

RUN chown -R app:app /home/app/.aspnet/https

USER app
WORKDIR /app
COPY ["jwt_properties.json", "."]
COPY ["nlog.config", "."]
EXPOSE 5060
EXPOSE 5061


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["Services/Orders/Orders.Presentation/Orders.Presentation.csproj", "Services/Orders/Orders.Presentation/"]
COPY ["Services/Orders/Orders.Infrastructure/Orders.Infrastructure.csproj", "Services/Orders/Orders.Infrastructure/"]
COPY ["Services/Orders/Orders.Application/Orders.Application.csproj", "Services/Orders/Orders.Application/"]
COPY ["EventBus/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "EventBus/EventBus.RabbitMQ/"]
COPY ["Services/Orders/Orders.Domain/Orders.Domain.csproj", "Services/Orders/Orders.Domain/"]
RUN dotnet restore "./Services/Orders/Orders.Presentation/Orders.Presentation.csproj"
COPY . .
WORKDIR "/src/Services/Orders/Orders.Presentation"
RUN dotnet build "./Orders.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Orders.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orders.Presentation.dll"]