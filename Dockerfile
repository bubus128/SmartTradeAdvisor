# Stage 1: Build .NET Core app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . .
RUN dotnet restore ./src/SmartTradeAdvisor.sln
RUN dotnet publish -c Release -o out -p:SourceLinkCreate=false ./src/SmartTradeAdvisor.sln

# Stage 2: Create runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "SmartTradeAdvisor.Api.dll"]