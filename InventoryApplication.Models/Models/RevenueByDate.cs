using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApplication.Models.Models
{
    public struct RevenueByDate
    {
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public string Month
        {
            get
            {
                return Date.ToString("MMM");
            }
        }
    }
}
