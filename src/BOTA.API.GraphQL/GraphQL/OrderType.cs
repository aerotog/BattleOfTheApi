using BOTA.Core.Models;
using GraphQL.Types;

namespace BOTA.API.GraphQL.GraphQL
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType()
        {
            Name = "Order";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("ID of the Order");
            Field(x => x.Date).Description("Date of the Order");
            Field(x => x.UserId).Description("User ID of the Order");
            //Field(x => x.Items, type: typeof(GraphQLListType)).Description("Items in the Order");
            Field<ListGraphType<ItemType>>("items");
            //Field(x => x.Items, type: typeof(ObjectGraphType)).Description("Items in the Order");
            //Field(x => x.Items).Description("Items in the Order");
        }
    }
}
