FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["./src/HUBVendas.Api/HUBVendas.Api.csproj", "src/HUBVendas.Api/"]
COPY ["./src/HUBVendas.Domain/HUBVendas.Domain.csproj", "src/HUBVendas.Domain/"]
COPY ["./src/HUBVendas.Infra/HUBVendas.Infra.csproj", "src/HUBVendas.Infra/"]
COPY ["./src/HUBVendas.Service/HUBVendas.Service.csproj", "src/HUBVendas.Service/"]
RUN dotnet restore "src/HUBVendas.Api/HUBVendas.Api.csproj"

COPY . .
WORKDIR "/src/src/HUBVendas.Api"

FROM build AS publish
RUN dotnet publish "HUBVendas.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HUBVendas.Api.dll"]