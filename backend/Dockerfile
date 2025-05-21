# Imagen base de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Imagen de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar código fuente
COPY . .

# Restaurar y compilar
RUN dotnet restore "PermissionSystem.API/PermissionSystem.API.csproj"
RUN dotnet publish "PermissionSystem.API/PermissionSystem.API.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PermissionSystem.API.dll"]
