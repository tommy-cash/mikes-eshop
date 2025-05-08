FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MikesEshop.Host/MikesEshop.Host.csproj", "MikesEshop.Host/"]
COPY ["MikesEshop.Products/MikesEshop.Products.csproj", "MikesEshop.Products/"]
COPY ["MikesEshop.Shared/MikesEshop.Shared.csproj", "MikesEshop.Shared/"]
RUN dotnet restore "MikesEshop.Host/MikesEshop.Host.csproj"
COPY . .
WORKDIR "/src/MikesEshop.Host"
RUN dotnet build "./MikesEshop.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MikesEshop.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MikesEshop.Host.dll"]
