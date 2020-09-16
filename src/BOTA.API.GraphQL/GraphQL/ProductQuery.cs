using System.Linq;
using BOTA.Core.Repository;
using GraphQL;
using GraphQL.Types;

namespace BOTA.API.GraphQL.GraphQL
{
    public class ProductQuery : ObjectGraphType
    {
        public ProductQuery(ShopContext shopContext)
        {
            Field<ProductType>(
                "Product",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType>
                    {
                        Name = "id", Description = "Id of the Product"
                    }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return shopContext.Products.FirstOrDefault(i => i.Id == id);
                });

            Field<ListGraphType<ProductType>>(
                "Products",
                resolve: context =>
                {
                    var users = shopContext.Products;
                    return users;
                });
        }
    }
}
