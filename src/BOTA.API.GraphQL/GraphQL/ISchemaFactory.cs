using BOTA.API.GraphQL.Dtos;
using GraphQL.Types;

namespace BOTA.API.GraphQL.GraphQL
{
    public interface ISchemaFactory
    {
        Schema GetSchema(GraphQLQuery query);
    }
}
