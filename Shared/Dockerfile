# Etapa 1: Build da biblioteca
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o projeto e restaura as dependências
COPY Shared/*.csproj ./Shared/
RUN dotnet restore ./Shared/Shared.csproj

# Copia o restante dos arquivos e compila a biblioteca
COPY Shared/. ./Shared/
RUN dotnet build ./Shared/Shared.csproj -c Release -o /app/build

# Etapa 2: Exporta só o build final
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app

# Copia a biblioteca compilada para uso nos outros serviços
COPY --from=build /app/build /app
