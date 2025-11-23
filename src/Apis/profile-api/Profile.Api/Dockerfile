FROM mcr.microsoft.com/dotnet/sdk:9.0.308 AS build

ARG BUILD_CONFIGURATION=Release
WORKDIR /app

# Copy csproj files for Profile API
COPY src/Apis/profile-api/Profile.Api/Profile.Api.csproj src/Apis/profile-api/Profile.Api/
COPY src/Apis/profile-api/Profile.Api.Core/Profile.Api.Core.csproj src/Apis/profile-api/Profile.Api.Core/
COPY src/Apis/profile-api/Profile.Api.DataPersistence/Profile.Api.DataPersistence.csproj src/Apis/profile-api/Profile.Api.DataPersistence/
COPY src/Apis/profile-api/Profile.Api.Domain/Profile.Api.Domain.csproj src/Apis/profile-api/Profile.Api.Domain/

# Copy csproj files for Libraries
COPY src/Libraries/Libraries.Common/Libraries.Common.csproj src/Libraries/Libraries.Common/
COPY src/Libraries/Libraries.DataInfrastructure/Libraries.DataInfrastructure.csproj src/Libraries/Libraries.DataInfrastructure/
COPY src/Libraries/Libraries.Api/Libraries.Api.csproj src/Libraries/Libraries.Api/

# Restore 
RUN dotnet restore "./src/Apis/profile-api/Profile.Api/Profile.Api.csproj"
COPY . .

# Copy anything else
WORKDIR /app/src/Apis/profile-api/Profile.Api
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0.11

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=build /app/publish .

ENTRYPOINT [ "dotnet", "Profile.Api.dll" ]
