namespace BOTA.API.gRPC.Extensions
{
    public static class ItemExtensions
    {
        public static Item ToProto(this Core.Models.Item item)
        {
            var proto = new Item
            {
                Id = item.Id,
                OrderId = item.OrderId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Product = item.Product.ToProto()
            };

            return proto;
        }

        public static Core.Models.Item ToEntity(this Item item)
        {

            return new Core.Models.Item
            {
                Id = item.Id,
                OrderId = item.OrderId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Product = item.Product.ToEntity()
            };
        }
    }
}
