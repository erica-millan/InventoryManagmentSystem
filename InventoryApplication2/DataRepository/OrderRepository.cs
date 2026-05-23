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
    public class OrderRepository : IOrderRepository
    {
        private static OrderRepository instance;
        public static OrderRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderRepository();
                }
                return instance;
            }
        }
        public OrderRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ToString();
        }
        private readonly string _connectionString;

        public void Add(Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("Insert into Order (OrderDate, OrderNumber, CustomerId, TotalAmount) VALUES (@OrderDate, @OrderNumber, @CustomerId, @TotalAmount)", connection))
            {
                command.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate));
                command.Parameters.Add(new SqlParameter("@OrderNumber", order.OrderNumber));
                command.Parameters.Add(new SqlParameter("@CustomerId", order.CustomerId));
                command.Parameters.Add(new SqlParameter("@TotalAmount", order.TotalAmount));
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("delete from Order where Id = @Id", connection))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }
        }

        public List<Order> Get(int customerId)
        {
            List<Order> order = new List<Order>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlDataAdapter("select * from [Order] where customerId = @CustomerId", connection))
            {
                DataTable dt = new DataTable();
                command.SelectCommand.Parameters.Add(new SqlParameter("@CustomerId", customerId));
                command.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows.OfType<DataRow>().ToList().ForEach(o =>
                    {
                        order.Add(new Order
                        {
                            Id = o.Field<int>("Id"),
                            OrderDate = o.Field<DateTime>("OrderDate"),
                            OrderNumber = o.Field<string>("OrderNumber"),
                            CustomerId = o.Field<int>("CustomerId"),
                            TotalAmount = o.Field<decimal>("TotalAmount"),
                        });
                    });
                }
            }
            return order;
        }

        public List<Order> GetAll()
        {
            List<Order> orders = new List<Order>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlDataAdapter("select * from Order", connection))
            {
                DataTable dt = new DataTable();
                command.Fill(dt);
                dt.Rows.OfType<DataRow>().ToList().ForEach(o =>
                {
                    orders.Add(new Order
                    {
                        Id = o.Field<int>("Id"),
                        OrderDate = o.Field<DateTime>("OrderDate"),
                        OrderNumber = o.Field<string>("OrderNumber"),
                        CustomerId = o.Field<int>("CustomerId"),
                        TotalAmount = o.Field<decimal>("TotalAmount"),
                    });
                });
            }
            return orders;
        }

        public List<Order> GetAll(int orderNumber)
        {

            List<Order> orders = new List<Order>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlDataAdapter("select * from Order where OrderNumber = @orderNumber", connection))
            {
                DataTable dt = new DataTable();
                command.SelectCommand.Parameters.Add(new SqlParameter("@orderNumber", orderNumber));
                command.Fill(dt);
                dt.Rows.OfType<DataRow>().ToList().ForEach(o =>
                {
                    orders.Add(new Order
                    {
                        Id = o.Field<int>("Id"),
                        OrderDate = o.Field<DateTime>("OrderDate"),
                        OrderNumber = o.Field<string>("OrderNumber"),
                        CustomerId = o.Field<int>("CustomerId"),
                        TotalAmount = o.Field<decimal>("TotalAmount"),
                    });
                });
            }
            return orders;
        }


        public void Update(Order order)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(@"Update Order set OrderDate=@OrderDate, OrderNumber=@OrderNumber, CustomerId=@CustomerId, TotalAmount=@TotalAmount
                                                  where Id = @Id", connection))
            {
                command.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate));
                command.Parameters.Add(new SqlParameter("@SupplierId", order.OrderNumber));
                command.Parameters.Add(new SqlParameter("@CustomerId", order.CustomerId));
                command.Parameters.Add(new SqlParameter("@TotalAmouunt", order.TotalAmount));
                command.Parameters.Add(new SqlParameter("@Id", order.Id));
                command.ExecuteNonQuery();
            }
        }
    }
}
