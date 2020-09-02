using System;
using System.Collections.Generic;

namespace BOTA.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        
        public List<Item> Items { get; set; }
    }
}
