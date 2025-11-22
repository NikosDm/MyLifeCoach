FROM mcr.microsoft.com/dotnet/sdk:8.0.204 AS build

ARG BUILD_CONFIGURATION=Release
WORKDIR /app

# Copy csproj files for Goals API
COPY src/Apis/goals-api/Goals.Api/Goals.Api.csproj src/Apis/goals-api/Goals.Api/
COPY src/Apis/goals-api/Goals.Api.Core/Goals.Api.Core.csproj src/Apis/goals-api/Goals.Api.Core/
COPY src/Apis/goals-api/Goals.Api.DataPersistence/Goals.Api.DataPersistence.csproj src/Apis/goals-api/Goals.Api.DataPersistence/
COPY src/Apis/goals-api/Goals.Api.Domain/Goals.Api.Domain.csproj src/Apis/goals-api/Goals.Api.Domain/

# Copy csproj files for Libraries
COPY src/Libraries/Libraries.Common/Libraries.Common.csproj src/Libraries/Libraries.Common/
COPY src/Libraries/Libraries.DataInfrastructure/Libraries.DataInfrastructure.csproj src/Libraries/Libraries.DataInfrastructure/
COPY src/Libraries/Libraries.Api/Libraries.Api.csproj src/Libraries/Libraries.Api/

# Restore 
RUN dotnet restore "./src/Apis/goals-api/Goals.Api/Goals.Api.csproj"
COPY . .

# Copy anything else
WORKDIR /app/src/Apis/goals-api/Goals.Api
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0.4

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=build /app/publish .

ENTRYPOINT [ "dotnet", "Goals.Api.dll" ]
