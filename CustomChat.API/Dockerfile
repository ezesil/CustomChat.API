#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CustomChat.API/CustomChat.API.csproj", "CustomChat.API/"]
COPY ["CustomChat.Application/CustomChat.Application.csproj", "CustomChat.Application/"]
COPY ["CustomChat.Domain/CustomChat.Domain.csproj", "CustomChat.Domain/"]
COPY ["CustomChat.Persistence/CustomChat.Persistence.csproj", "CustomChat.Persistence/"]
RUN dotnet restore "./CustomChat.API/CustomChat.API.csproj"
COPY . .
WORKDIR "/src/CustomChat.API"
RUN dotnet build "./CustomChat.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CustomChat.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomChat.API.dll"]