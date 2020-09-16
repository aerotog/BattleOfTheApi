using BOTA.Core.Models;
using GraphQL.Types;

namespace BOTA.API.GraphQL.GraphQL
{
    public class ItemType : ObjectGraphType<Item>
    {
        public ItemType()
        {
            Name = "Item";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("ID of the Item");
            Field(x => x.OrderId).Description("Order ID of the Item");
            Field(x => x.ProductId).Description("Product ID of the Item");
            Field(x => x.Quantity).Description("Quantity of the Item");
            //Field<ProductType>("product");
            Field(x => x.Product, type: typeof(ProductType)).Description("Product of the Item");
        }
    }
}
