using InventoryApplication.Models.Models;
using InventoryApplication2.DataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApplication2.Business_Logic
{
    public class OrderFactory
    { 
        public static List<OrderItem> GetOrderItems(int orderId) 
        {
            return OrderItemRepository.Instance.GetAll(orderId);
        }
        public static List<OrderProducts> GetOrderProducts(int orderId) 
        { 
        var orderItems = OrderItemRepository.Instance.GetAll(orderId);
            List<OrderProducts> orderProducts = new List<OrderProducts>();
            foreach(var items in orderItems)
            {
                var product = ProductRepository.Instance.Get(items.ProductId);
                orderProducts.Add(new OrderProducts
                {
                    Id = items.Id,
                    ProductId = items.ProductId,
                    OrderId = items.OrderId,
                    ProductName = product.ProductName,
                    SupplierId = product.SupplierId,
                    Quantity = items.Quantity,
                    UnitPrice = items.UnitPrice
                });
            }
            return orderProducts;
        }
    }
}
