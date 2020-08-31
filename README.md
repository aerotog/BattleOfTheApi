# BattleOfTheApi
Simple C# implementations of various HTTP based API using a common data set hosted in PostgreSQL.

## To Run
Pick one:
- `docker-compose up` from within /src/
    - Use `docker ps` to see which host ports are being bound to each API
- Open the Visual Studio Solution file (.sln), run the docker-compose project
- Target and run a single API .csproj with either the `dotnet` CLI, VS Code, or Visual Studio
    - NOTE: If you run an individual project, you'll need to setup and configure the connection for your own Postgres DB instance as docker-compose was doing that for you

## Available API
- Open API

## Planned API
- SOAP
- REST
    - HAL
    - ODATA
    - JSON API
- Open API (Swagger)
- ODATA
- GRAPHQL
- gRPC