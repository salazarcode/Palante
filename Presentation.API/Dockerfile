FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY GestionCartera.API/*.csproj ./GestionCartera.API/
COPY Domain/*.csproj ./Domain/
COPY DataAccess/*.csproj ./DataAccess/
COPY Transversal/*.csproj ./Transversal/

RUN dotnet restore

# Copy everything else and build
COPY GestionCartera.API/. ./GestionCartera.API/
COPY Domain/. ./Domain/
COPY DataAccess/. ./DataAccess/
COPY Transversal/. ./Transversal/

WORKDIR /app/GestionCartera.API
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/GestionCartera.API/out ./
ENTRYPOINT ["dotnet", "GestionCartera.API.dll"]
