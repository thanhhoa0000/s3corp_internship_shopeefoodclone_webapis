# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

RUN mkdir -p /home/app/.aspnet/https

COPY ./certs/.aspnet/https/Products.API.pfx /home/app/.aspnet/https/Products.API.pfx

RUN chown -R app:app /home/app/.aspnet/https

USER app
WORKDIR /app
COPY ["jwt_properties.json", "."]
COPY ["nlog.config", "."]
EXPOSE 5040
EXPOSE 5041


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["Services/Products/Products.Presentation/Products.Presentation.csproj", "Services/Products/Products.Presentation/"]
COPY ["Services/Products/Products.Infrastructure/Products.Infrastructure.csproj", "Services/Products/Products.Infrastructure/"]
COPY ["Services/Products/Products.Application/Products.Application.csproj", "Services/Products/Products.Application/"]
COPY ["Services/Products/Products.Domain/Products.Domain.csproj", "Services/Products/Products.Domain/"]
RUN dotnet restore "./Services/Products/Products.Presentation/Products.Presentation.csproj"
COPY . .
WORKDIR "/src/Services/Products/Products.Presentation"
RUN dotnet build "./Products.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Products.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Products.Presentation.dll"]