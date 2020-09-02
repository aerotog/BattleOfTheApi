using System;
using System.Collections.Generic;
using JsonApiDotNetCore.Models;

namespace BOTA.API.REST.JsonApi.Models
{
    public class Order : Identifiable
    {
        [Attr]
        public int UserId { get; set; }

        [Attr]
        public DateTime Date { get; set; }

        [HasMany]
        public List<Item> Items { get; set; }
    }
}
