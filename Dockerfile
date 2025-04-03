# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar el archivo de la solución
COPY price-list.sln .

# Copiar el archivo del proyecto antes de restaurar
COPY price-list.csproj .

# Restaurar dependencias
RUN dotnet restore "price-list.csproj"

# Copiar el resto de los archivos del proyecto
COPY . .

# Compilar el proyecto
RUN dotnet publish "price-list.csproj" -c Release -o /publish --no-restore

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar los archivos compilados
COPY --from=build /publish .

# Exponer el puerto de la API
EXPOSE 8080

# Comando de inicio
CMD ["dotnet", "price-list.dll"]
