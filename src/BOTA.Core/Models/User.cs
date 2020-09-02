using System.Collections.Generic;

namespace BOTA.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }

        public List<Order> Orders { get; set; }
    }
}
