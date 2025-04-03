# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copiar todo el código fuente
COPY . .

# Restaurar paquetes
RUN dotnet restore "price.list.csproj"

# Compilar y publicar la aplicación
RUN dotnet publish "price.list.csproj" -c Release -o /publish --no-restore

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Copiar la aplicación compilada
COPY --from=build /publish .

# Exponer el puerto
EXPOSE 8080

# Iniciar la aplicación
CMD ["dotnet", "price.list.dll"]
