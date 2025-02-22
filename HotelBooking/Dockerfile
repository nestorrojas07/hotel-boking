# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# build project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HotelBooking/HotelBooking.csproj", "HotelBooking/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infraestructure/Infraestructure.csproj", "Infraestructure/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "./HotelBooking/HotelBooking.csproj"
COPY . .

WORKDIR "/src/HotelBooking"
RUN dotnet build "./HotelBooking.csproj" -c $BUILD_CONFIGURATION -o /app/build

# publish files
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HotelBooking.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# publish binary files to final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelBooking.dll"]
