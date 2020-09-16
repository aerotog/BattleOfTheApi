using System;
using BOTA.API.GraphQL.Dtos;
using BOTA.Core.Repository;
using GraphQL.Types;

namespace BOTA.API.GraphQL.GraphQL
{
    public class SchemaFactory : ISchemaFactory
    {
        private readonly ShopContext _dbContext;

        public SchemaFactory(ShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Schema GetSchema(GraphQLQuery query)
        {
            ObjectGraphType graphType;

            switch (query.OperationName)
            {
                case nameof(OrderQuery):
                    graphType = new OrderQuery(_dbContext);
                    break;
                case nameof(ProductQuery):
                    graphType = new ProductQuery(_dbContext);
                    break;
                case nameof(UserQuery):
                    graphType = new UserQuery(_dbContext);
                    break;
                default:
                    throw new NotImplementedException($"No Graph Type for query type {query.Query}");

            }

            return new Schema
            {
                Query = graphType
            };
        }
    }
}