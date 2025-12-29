FROM mcr.microsoft.com/dotnet/sdk:9.0.308 AS build

ARG BUILD_CONFIGURATION=Release
WORKDIR /app

# Copy csproj files for Identity Server 
COPY src/Apis/identity-server/IdentityServer/IdentityServer.csproj src/Apis/identity-server/IdentityServer/
COPY src/Apis/identity-server/IdentityServer.Core/IdentityServer.Core.csproj src/Apis/identity-server/IdentityServer.Core/
COPY src/Apis/identity-server/IdentityServer.DataAccess/IdentityServer.DataAccess.csproj src/Apis/identity-server/IdentityServer.DataAccess/

# Copy csproj files for Libraries
COPY src/Libraries/Libraries.Common/Libraries.Common.csproj src/Libraries/Libraries.Common/

# Restore 
RUN dotnet restore "./src/Apis/identity-server/IdentityServer/IdentityServer.csproj"
COPY . .

# Copy anything else
WORKDIR /app/src/Apis/identity-server/IdentityServer
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0.11

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=build /app/publish .

ENTRYPOINT [ "dotnet", "IdentityServer.dll" ]