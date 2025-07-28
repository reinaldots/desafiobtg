# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia tudo e restaura as dependências
COPY . ./
RUN dotnet restore DesafioBTG.API.csproj
RUN dotnet publish DesafioBTG.API.csproj -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Porta padrão da API
EXPOSE 80

ENTRYPOINT ["dotnet", "DesafioBTG.API.dll"]
