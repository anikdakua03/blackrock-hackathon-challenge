# docker build -t blk-hacking-ind-{name-lastname} -f Blackrock-Hackathon/Dockerfile .
#
# Base image selection:
# - OS: Debian-based Linux (selected because official Microsoft .NET images use Debian/Ubuntu
#   variants by default, providing a small, secure, and widely-compatible runtime surface).
# - Criteria: minimal footprint, official vendor support for .NET 10, regular security updates,
#   and compatibility with Linux container hosts used in CI/CD and cloud deployments.
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
# Expose the application port and instruct Kestrel to listen on it
EXPOSE 5477
ENV ASPNETCORE_URLS=http://+:5477


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Blackrock-Hackathon/Blackrock-Hackathon.csproj", "Blackrock-Hackathon/"]
RUN dotnet restore "./Blackrock-Hackathon/Blackrock-Hackathon.csproj"
COPY . .
WORKDIR "/src/Blackrock-Hackathon"
RUN dotnet build "./Blackrock-Hackathon.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Blackrock-Hackathon.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blackrock-Hackathon.dll"]