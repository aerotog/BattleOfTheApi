using BOTA.Core.Models;
using GraphQL.Types;

namespace BOTA.API.GraphQL.GraphQL
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType()
        {
            Name = "Product";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("ID of the Product");
            Field(x => x.Name).Description("Name of the Product");
            Field(x => x.Price).Description("Price of the Product");
        }
    }
}
