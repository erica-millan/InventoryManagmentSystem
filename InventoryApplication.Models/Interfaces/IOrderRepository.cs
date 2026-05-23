using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryApplication.Models.Models;

namespace InventoryApplication.Models.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void Update(Order order);
        void Delete(int id);

        List<Order> GetAll();
        List<Order> GetAll(int orderNumber);
        List<Order> Get(int customerId);
    }
}
