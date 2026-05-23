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
    public class CustomerRepository : ICustomerRepository
    {
        private static CustomerRepository instance;
        public static CustomerRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomerRepository();
                }
                return instance;
            }
        }

        public CustomerRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ToString();
        }

        private readonly string _connectionString;
        public void Add(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("Insert into Customer (FirstName, LastName, City, Country, Phone) " +
                                                "values (@FirstName, @LastName, @City, @Country, @Phone)", connection))
            {
                command.Parameters.Add(new SqlParameter("@FirstName", customer.FirstName));
                command.Parameters.Add(new SqlParameter("@LastName", customer.LastName));
                command.Parameters.Add(new SqlParameter("@City", customer.City));
                command.Parameters.Add(new SqlParameter("@Country", customer.Country));
                command.Parameters.Add(new SqlParameter("@Phone", customer.Phone));
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("delete from Customer where Id = @Id", connection))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }
        }

        public Customer Get(int customerId)
        {
            Customer customer = null;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlDataAdapter("select * from Customer where Id = @Id", connection))
            {
                DataTable dt = new DataTable();
                command.SelectCommand.Parameters.Add(new SqlParameter("@Id", customerId));
                command.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    var p = dt.Rows[0];
                    customer = new Customer 
                    {
                        Id = p.Field<int>("Id"),
                        FirstName = p.Field<string>("FirstName"),
                        LastName = p.Field<string>("LastName"),
                        City = p.Field<string>("City"),
                        Country = p.Field<string>("Country"),
                        Phone = p.Field<string>("Phone"),
                    };
                }
            }
            return customer;
        }

        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlDataAdapter("select * from Customer", connection))
            {
                DataTable dt = new DataTable();
                command.Fill(dt);
                dt.Rows.OfType<DataRow>().ToList().ForEach(p =>
                {
                    customers.Add(new Customer
                    {
                        Id = p.Field<int>("Id"),
                        FirstName = p.Field<string>("FirstName"),
                        LastName = p.Field<string>("LastName"),
                        City = p.Field<string>("City"),
                        Country = p.Field<string>("Country"),
                        Phone = p.Field<string>("Phone"),
                    });
                });
            }
            return customers;
        }

        public List<Customer> GetAll(string firstName)
        {
            List<Customer> customers = new List<Customer>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlDataAdapter("select * from Customer where FirstName = @FirstName", connection))
            {
                DataTable dt = new DataTable();
                command.SelectCommand.Parameters.Add(new SqlParameter("@FirstName", firstName));
                command.Fill(dt);
                dt.Rows.OfType<DataRow>().ToList().ForEach(p =>
                {
                    customers.Add(new Customer
                    {
                        Id = p.Field<int>("Id"),
                        FirstName = p.Field<string>("FirstName"),
                        LastName = p.Field<string>("LastName"),
                        City = p.Field<string>("City"),
                        Country = p.Field<string>("Country"),
                        Phone = p.Field<string>("Phone"),
                    });
                });
            }
            return customers;
        }

        public void Update(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(@"Update Customer set FirstName=@FirstName, LastName=@LastName, City=@City, Country=@Country, Phone=@Phone
                                                  where Id = @Id", connection))
            {
                command.Parameters.Add(new SqlParameter("@FirstName", customer.FirstName));
                command.Parameters.Add(new SqlParameter("@LastName", customer.LastName));
                command.Parameters.Add(new SqlParameter("@City", customer.City));
                command.Parameters.Add(new SqlParameter("@Country", customer.Country));
                command.Parameters.Add(new SqlParameter("@Phone", customer.Phone));
                command.Parameters.Add(new SqlParameter("@Id", customer.Id));
                command.ExecuteNonQuery();
            }
        }
    }
}
