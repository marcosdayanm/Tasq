# Utiliza una imagen base de ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Compila tu aplicación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Tasq.csproj", "./"]
RUN dotnet restore "Tasq.csproj"
COPY . .
RUN dotnet build "Tasq.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tasq.csproj" -c Release -o /app/publish

# Construye la imagen final
FROM base AS final
WORKDIR /app

# Para la base de datos
COPY ./db_sqlite3.db /app/

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasq.dll"]
