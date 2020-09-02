# BattleOfTheApi
Simple C# implementations of various HTTP based API using a common data set hosted in PostgreSQL.
- [BattleOfTheApi](#battleoftheapi)
  - [To Run](#to-run)
  - [Available API](#available-api)
  - [Planned API](#planned-api)
  - [Implementation Notes](#implementation-notes)
      - [Open API](#open-api)
    - [REST](#rest)
      - [JSON API](#json-api)
## To Run
Pick one:
- `docker-compose up` from within /src/
    - Use `docker ps` to see which host ports are being bound to each API
- Open the Visual Studio Solution file (.sln), run the docker-compose project
- Target and run a single API .csproj with either the `dotnet` CLI, VS Code, or Visual Studio
    - NOTE: If you run an individual project, you'll need to setup and configure the connection for your own Postgres DB instance as docker-compose is otherwise doing that

## Available API
- Open API
- REST
  - JSON API


## Planned API
- SOAP
- REST
    - HAL https://github.com/JakeGinnivan/WebApi.Hal
    - ODATA https://github.com/OData/WebApi
- GRAPHQL https://github.com/graphql-dotnet/graphql-dotnet
- gRPC

## Implementation Notes

#### Open API
-  [Specification](https://github.com/OAI/OpenAPI-Specification)
-  [Library used](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

Uses auto generated (via Swashbuckle) Open API docs along with the Swagger UI to make exploring the API easy. Clients generate URLs from endpoints specified in the Open API document.

### REST
Per the creator of REST, Roy Fielding, for an API to truly be rest, [it must use Hypermedia As The Engine of Application State (HATEOAS)](https://roy.gbiv.com/untangled/2008/rest-apis-must-be-hypertext-driven). This in theory should make the API self documenting such that a client could organically discover what functions are avialable as they use the API.

These API implemented here seem to at least attempt to do so by including links in their representations.

#### JSON API
- [Specification](https://jsonapi.org/format/)
- [Library used](https://github.com/json-api-dotnet/JsonApiDotNetCore)
- [Library docs](https://json-api-dotnet.github.io/JsonApiDotNetCore/getting-started/install.html)
  
It does add links for its resources, but only for data relationships. There are no rels that tell the user what actions are available. Does this actually qualify as HATEOAS?

The JSON API project intentionally does not have a Swagger doc. 
There is a Postman collection in the project directory that can be

It does generate some very useful features with very little boiler plate code. The boiler plating is reduced even further if you use Entity Framework. If you follow their conventions, you get the following features for "free":
- Linking of related objects
- Pagination
- Sorting
- Filtering
- Fieldset selection (kinda like GraphQL)
- Read/Write access control via attributes