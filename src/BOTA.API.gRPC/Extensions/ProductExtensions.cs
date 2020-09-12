namespace BOTA.API.gRPC.Extensions
{
    public static class ProductExtensions
    {
        public static Product ToProto(this Core.Models.Product product)
        {
            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public static Core.Models.Product ToEntity(this Product product)
        {
            return new Core.Models.Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
