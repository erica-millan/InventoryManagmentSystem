using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryApplication.Models.Models;

namespace InventoryApplication.Models.Interfaces
{
    public interface IOrderItemRepository
    {
        void Add(OrderItem orderItem);
        void Update(OrderItem orderItem);
        void Delete(int id);

        List<OrderItem> GetAll();
        List<OrderItem> GetAll(int orderId);
        OrderItem Get(int productId);
      
    }
}
