using InventoryApplication.Models.Interfaces;
using InventoryApplication.Models.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApplication2.DataRepository
{
    class OrderItemRepository : IOrderItemRepository
    {
        private static OrderItemRepository instance;
        public static OrderItemRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderItemRepository();
                }
                return instance;
            }
        }
        public OrderItemRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ToString();
        }
        private readonly string _connectionString;

        public void Add(OrderItem orderItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("Insert into OrderItem (OrderId, ProductId, UnitPrice, Quantity) VALUES (@OrderId, @ProductId, @UnitPrice, @Quantity)", connection))
            {
                command.Parameters.Add(new SqlParameter("@OrderId", orderItem.OrderId));
                command.Parameters.Add(new SqlParameter("@ProductId", orderItem.ProductId));
                command.Parameters.Add(new SqlParameter("@UnitPrice", orderItem.UnitPrice));
                command.Parameters.Add(new SqlParameter("@Quantity", orderItem.Quantity));
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("delete from OrderItem where Id = @Id", connection))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }
        }

        public OrderItem Get(int productId)
        {
            OrderItem orderItem = null;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlDataAdapter("select * from OrderItem where Id = @ProductId", connection))
            {
                DataTable dt = new DataTable();
                command.SelectCommand.Parameters.Add(new SqlParameter("@Id", productId));
                command.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    var o = dt.Rows[0];
                    orderItem = new OrderItem
                    {
                        Id = o.Field<int>("Id"),
                        OrderId = o.Field<int>("OrderId"),
                        ProductId = o.Field<int>("ProductId"),
                        UnitPrice = o.Field<decimal>("UnitPrice"),
                        Quantity = o.Field<int>("Quantity"),
                    };
                }
            }
            return orderItem;
        }

        public List<OrderItem> GetAll()
        {
            List<OrderItem> orderItem = new List<OrderItem>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlDataAdapter("select * from OrderItem", connection))
            {
                DataTable dt = new DataTable();
                command.Fill(dt);
                dt.Rows.OfType<DataRow>().ToList().ForEach(o =>
                {
                    orderItem.Add(new OrderItem
                    {
                        Id = o.Field<int>("Id"),
                        OrderId = o.Field<int>("OrderId"),
                        ProductId = o.Field<int>("ProductId"),
                        UnitPrice = o.Field<decimal>("UnitPrice"),
                        Quantity = o.Field<int>("Quantity"),
                    });
                });
            }
            return orderItem;
        }
         public List<OrderItem> GetAll(int orderId)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlDataAdapter("select * from OrderItem where OrderId = @orderId", connection))
            {
                DataTable dt = new DataTable();
                command.SelectCommand.Parameters.Add(new SqlParameter("@OrderId", orderId));
                command.Fill(dt);
                dt.Rows.OfType<DataRow>().ToList().ForEach(o =>
                {
                    orderItems.Add(new OrderItem
                    {
                        Id = o.Field<int>("Id"),
                        OrderId = o.Field<int>("OrderId"),
                        ProductId = o.Field<int>("ProductId"),
                        UnitPrice = o.Field<decimal>("UnitPrice"),
                        Quantity = o.Field<int>("Quantity"),
                    });
                });
            }
            return orderItems;
        }
        

        public void Update(OrderItem orderItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(@"Update OrderItem set OrderId=@OrderId, ProductId=@ProductId, UnitPrice=@UnitPrice, Quantity=@Quantity
                                                  where Id = @Id", connection))
            {
                command.Parameters.Add(new SqlParameter("@OrderId", orderItem.OrderId));
                command.Parameters.Add(new SqlParameter("@ProductId", orderItem.ProductId));
                command.Parameters.Add(new SqlParameter("@UnitPrice", orderItem.UnitPrice));
                command.Parameters.Add(new SqlParameter("@Quantity", orderItem.Quantity));
                command.ExecuteNonQuery();
            }
        }
    }
}
