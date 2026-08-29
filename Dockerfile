# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .

# Render asigna dinámicamente el puerto mediante la variable PORT
ENV ASPNETCORE_URLS=http://+:${PORT}
ENTRYPOINT ["dotnet", "TecnoGasHogar.dll"]
