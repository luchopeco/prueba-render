# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar archivos del proyecto y restaurar dependencias
COPY *.sln .
COPY src/ ./src/
RUN dotnet restore

# Compilar la aplicación
RUN dotnet publish src/TuProyecto.Api/TuProyecto.Api.csproj -c Release -o /publish --no-restore

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar los archivos publicados desde la etapa anterior
COPY --from=build /publish .

# Exponer el puerto que usará la API
EXPOSE 8080

# Comando de inicio
CMD ["dotnet", "price.list.dll"]
