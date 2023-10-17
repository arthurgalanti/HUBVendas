# Imagem base
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiando os csproj e restaurando as dependencias
COPY ["./src/HUBVendas.Api/HUBVendas.Api.csproj", "src/HUBVendas.Api/"]
COPY ["./src/HUBVendas.Domain/HUBVendas.Domain.csproj", "src/HUBVendas.Domain/"]
COPY ["./src/HUBVendas.Infra/HUBVendas.Infra.csproj", "src/HUBVendas.Infra/"]
COPY ["./src/HUBVendas.Service/HUBVendas.Service.csproj", "src/HUBVendas.Service/"]
RUN dotnet restore "src/HUBVendas.Api/HUBVendas.Api.csproj"

# Copiando todo o código
COPY . .
WORKDIR "/src/src/HUBVendas.Api"

# Etapa de publish
FROM build AS publish
RUN dotnet publish "HUBVendas.Api.csproj" -c Release -o /app/publish

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HUBVendas.Api.dll"]