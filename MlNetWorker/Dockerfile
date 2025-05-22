# Etapa base modificada para instalar bibliotecas nativas antes de usar USER
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Só defina o usuário DEPOIS das instalações
WORKDIR /app
USER $APP_UID

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MlNetWorker/MlNetWorker.csproj", "MlNetWorker/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "./MlNetWorker/MlNetWorker.csproj"
COPY . .
WORKDIR "/src/MlNetWorker"
RUN dotnet build "./MlNetWorker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MlNetWorker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final: copie o que foi publicado e rode o app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MlNetWorker.dll"]
