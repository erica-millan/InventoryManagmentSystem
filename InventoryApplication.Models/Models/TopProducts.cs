using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApplication.Models.Models
{
    //this model is used for the topproducts dashboard repo method.
    public struct TopProducts
    {
       public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int TotalCount { get; set; }
    }
}
