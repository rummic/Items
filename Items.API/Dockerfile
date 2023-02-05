#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Items.API/Items.API.csproj", "Items.API/"]
RUN dotnet restore "Items.API/Items.API.csproj"
COPY . .
WORKDIR "/src/Items.API"
RUN dotnet build "Items.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Items.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Items.API.dll"]