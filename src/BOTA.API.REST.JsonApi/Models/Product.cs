using JsonApiDotNetCore.Models;

namespace BOTA.API.REST.JsonApi.Models
{
    public class Product : Identifiable
    {
        [Attr]
        public string Name { get; set; }

        [Attr]
        public int Price { get; set; }
    }
}
