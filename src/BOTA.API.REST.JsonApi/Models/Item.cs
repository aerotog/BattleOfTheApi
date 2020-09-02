using JsonApiDotNetCore.Models;

namespace BOTA.API.REST.JsonApi.Models
{
    public class Item : Identifiable
    // Notice the models have no "Id" field. The `Identifiable` base class adds the Id field
    {
        [Attr]
        public int OrderId { get; set; }

        [Attr]
        public int ProductId { get; set; }

        [Attr]
        public int Quantity { get; set; }

        // JSON API relies on model type attributes to define relationships between API representations
        [HasOne]
        public Product Product { get; set; }
    }
}
