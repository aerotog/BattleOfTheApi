using System.Linq;
using BOTA.Core.Repository;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace BOTA.API.GraphQL.GraphQL
{
    public class OrderQuery : ObjectGraphType
    {
        public OrderQuery(ShopContext shopContext)
        {
            Field<OrderType>(
                "Order",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType>
                    {
                        Name = "id", Description = "Id of the Order"
                    }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return shopContext
                        .Orders
                        .Include(x => x.Items)
                        .ThenInclude(x => x.Product)
                        .FirstOrDefault(i => i.Id == id);
                });

            Field<ListGraphType<OrderType>>(
                "Orders",
                resolve: context =>
                {
                    var users = shopContext.Orders;
                    return users;
                });
        }
    }
}
