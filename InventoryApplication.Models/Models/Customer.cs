using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApplication.Models.Models
{
    public class Customer
    {//properties. They contain the set and get accessors.
     // set accessor is used to validate and assign the value in to the field.
        // get accessor is used to retrieve the value from the field.
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
