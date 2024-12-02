#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MeasurementService/MeasurementService.csproj", "MeasurementService/"]
RUN dotnet restore "./MeasurementService/MeasurementService.csproj"
COPY . .
WORKDIR "/src/MeasurementService"
RUN dotnet build "./MeasurementService.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN ls -alh /app/build


# publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MeasurementService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false



#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeasurementService.dll"]