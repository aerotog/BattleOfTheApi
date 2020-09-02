using System.Collections.Generic;
using JsonApiDotNetCore.Models;

namespace BOTA.API.REST.JsonApi.Models
{
    public class User : Identifiable
    {
        [Attr]
        public string FirstName { get; set; }

        [Attr]
        public string LastName { get; set; }

        [Attr]
        public string City { get; set; }

        [HasMany]
        public List<Order> Orders { get; set; }
    }
}
