#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "Items.sln" "./"
COPY "Items.Data/Items.Data.csproj" "./Items.Data/"
COPY "Items.API.Test/Items.API.Test.csproj" "./Items.API.Test/"
COPY "Items.API/Items.API.csproj" "./Items.API/"

RUN dotnet restore
COPY . .
WORKDIR "/src/Items.Data"
RUN dotnet build -c Debug -o /app

WORKDIR "/src/Items.API.Test"
RUN dotnet build -c Debug -o /app

WORKDIR "/src/Items.API"
RUN dotnet build -c Debug -o /app

FROM build AS publish
RUN dotnet publish -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Items.API.dll"]