using InventoryApplication.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryApplication2.DataRepository;

namespace InventoryApplication2.Business_Logic
{
    public class CustomerFactory
    {
        public static Customer GetCustomer(int customerId) 
        {
            var customer = CustomerRepository.Instance.Get(customerId);
            customer.Orders = OrderRepository.Instance.Get(customerId);

            return customer;
        }
    }
}
