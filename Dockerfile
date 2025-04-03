# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar la solución y los proyectos
COPY . .  

# Restaurar dependencias
RUN dotnet restore price-list.csproj

# Compilar el proyecto
RUN dotnet publish price-list.csproj -c Release -o /publish --no-restore

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar archivos publicados desde la fase anterior
COPY --from=build /publish .

# Exponer el puerto de la API
EXPOSE 8080

# Comando de inicio
CMD ["dotnet", "price-list.dll"]
