#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . ./
#COPY ["api/SmartTradeAdvisor.Api/SmartTradeAdvisor.Api.csproj", "api/SmartTradeAdvisor.Api/"]
#COPY ["infrastucture/SmartTradeAdvisor.Infrastructure/SmartTradeAdvisor.Data.csproj", "infrastucture/SmartTradeAdvisor.Infrastructure/"]
RUN dotnet restore "./SmartTradeAdvisor.sln"
#RUN dotnet restore "./api/SmartTradeAdvisor.Api/./SmartTradeAdvisor.Api.csproj"
#WORKDIR "/src/api/SmartTradeAdvisor.Api"
RUN dotnet build "./SmartTradeAdvisor.sln" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SmartTradeAdvisor.sln" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartTradeAdvisor.Api.dll"]