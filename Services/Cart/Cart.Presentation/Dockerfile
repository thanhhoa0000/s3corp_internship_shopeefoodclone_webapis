# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

RUN mkdir -p /home/app/.aspnet/https

COPY ./certs/.aspnet/https/Cart.API.pfx /home/app/.aspnet/https/Cart.API.pfx

RUN chown -R app:app /home/app/.aspnet/https

USER app
WORKDIR /app
COPY ["nlog.config", "."]
EXPOSE 5050
EXPOSE 5051


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["Services/Cart/Cart.Presentation/Cart.Presentation.csproj", "Services/Cart/Cart.Presentation/"]
COPY ["Services/Cart/Cart.Infrastructure/Cart.Infrastructure.csproj", "Services/Cart/Cart.Infrastructure/"]
COPY ["Services/Cart/Cart.Application/Cart.Application.csproj", "Services/Cart/Cart.Application/"]
COPY ["EventBus/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "EventBus/EventBus.RabbitMQ/"]
COPY ["Services/Cart/Cart.Domain/Cart.Domain.csproj", "Services/Cart/Cart.Domain/"]
RUN dotnet restore "./Services/Cart/Cart.Presentation/Cart.Presentation.csproj"
COPY . .
WORKDIR "/src/Services/Cart/Cart.Presentation"
RUN dotnet build "./Cart.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cart.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cart.Presentation.dll"]