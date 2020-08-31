#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BOTA.API.OpenAPI/BOTA.API.OpenAPI.csproj", "BOTA.API.OpenAPI/"]
COPY ["BOTA.Core/BOTA.Core.csproj", "BOTA.Core/"]
COPY ["BOTA.DB/BOTA.DB.csproj", "BOTA.DB/"]
RUN dotnet restore "BOTA.API.OpenAPI/BOTA.API.OpenAPI.csproj"
COPY . .
WORKDIR "/src/BOTA.API.OpenAPI"
RUN dotnet build "BOTA.API.OpenAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BOTA.API.OpenAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BOTA.API.OpenAPI.dll"]