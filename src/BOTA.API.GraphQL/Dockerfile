#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BOTA.API.GraphQL/BOTA.API.GraphQL.csproj", "BOTA.API.GraphQL/"]
RUN dotnet restore "BOTA.API.GraphQL/BOTA.API.GraphQL.csproj"
COPY . .
WORKDIR "/src/BOTA.API.GraphQL"
RUN dotnet build "BOTA.API.GraphQL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BOTA.API.GraphQL.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BOTA.API.GraphQL.dll"]