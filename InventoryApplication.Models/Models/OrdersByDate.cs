using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApplication.Models.Models
{
    public struct OrdersByDate
    {
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
       // Adding a property type string going to return a string called month. 
        public string Month
        {
            get
            {
                return Date.ToString("MMM");
            }
        }
    }

}
