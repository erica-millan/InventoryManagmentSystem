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
    public class SupplierRepository : ISupplierRepository
    {
        public SupplierRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ToString();
        }
        private readonly string _connectionString;
        public void Add(Supplier supplier)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("INSERT INTO Supplier (CompanyName, ContactName, ContactTitle, City, Country, Phone, Fax)" +
                "values (@CompanyName, @ContactName, @ContactTitle, @City, @Country, @Phone, @Fax)", connection))
            {
                command.Parameters.Add(new SqlParameter("@CompanyName", supplier.CompanyName));
                command.Parameters.Add(new SqlParameter("@ContactName", supplier.ContactName));
                command.Parameters.Add(new SqlParameter("@ContactTitle", supplier.ContactTitle));
                command.Parameters.Add(new SqlParameter("@City", supplier.City));
                command.Parameters.Add(new SqlParameter("@Country", supplier.Country));
                command.Parameters.Add(new SqlParameter("@Phone", supplier.Phone));
                command.Parameters.Add(new SqlParameter("@Fax", supplier.Fax));
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("delete from Supplier where Id = @Id", connection))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }
        }

        public Supplier Get(int supplierId)
        {
            Supplier supplier = null;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlDataAdapter("select * from Supplier where Id = @Id", connection))
            {
                DataTable dt = new DataTable();
                command.SelectCommand.Parameters.Add(new SqlParameter("@Id", supplierId));
                command.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    var p = dt.Rows[0];
                    supplier = new Supplier
                    {
                        Id = p.Field<int>("Id"),
                        CompanyName = p.Field<string>("CompanyName"),
                        ContactName = p.Field<string>("ContactName"),
                        ContactTitle = p.Field<string>("ContactTitle"),
                        City = p.Field<string>("City"),
                        Country = p.Field<string>("Country"),
                        Phone = p.Field<string>("Phone"),
                        Fax = p.Field<string>("Fax"),
                    };
                }
            }
            return supplier;
        }

        public List<Supplier> GetAll()
        {
            List<Supplier> suppliers = new List<Supplier>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlDataAdapter("select * from supplier", connection))
            {
                DataTable dt = new DataTable();
                command.Fill(dt);
                dt.Rows.OfType<DataRow>().ToList().ForEach(s =>
                {
                    suppliers.Add(new Supplier
                    {
                        Id = s.Field<int>("Id"),
                        CompanyName = s.Field<string>("CompanyName"),
                        ContactName = s.Field<string>("ContactName"),
                        ContactTitle = s.Field<string>("ContactTitle"),
                        City = s.Field<string>("City"),
                        Country = s.Field<string>("Country"),
                        Phone = s.Field<string>("Phone"),
                        Fax = s.Field<string>("Fax"),
                    });
                });
            }
            return suppliers;
        }

        public List<Supplier> GetAll(string companyName)
        {
            List<Supplier> suppliers = new List<Supplier>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlDataAdapter("select * from Supplier where CompanyName like @CompanyName", connection))
            {
                DataTable dt = new DataTable();
                command.SelectCommand.Parameters.Add(new SqlParameter("@ProductName", companyName + "%"));
                command.Fill(dt);
                dt.Rows.OfType<DataRow>().ToList().ForEach(s =>
                {
                    suppliers.Add(new Supplier
                    {
                        Id = s.Field<int>("Id"),
                        CompanyName = s.Field<string>("CompanyName"),
                        ContactName = s.Field<string>("ContactName"),
                        ContactTitle = s.Field<string>("ContactTitle"),
                        City = s.Field<string>("City"),
                        Country = s.Field<string>("Country"),
                        Phone = s.Field<string>("Phone"),
                        Fax = s.Field<string>("Fax"),
                    });
                });
            }
            return suppliers;
        }

        public void Update(Supplier supplier)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(@"Update Supplier set CompanyName=@CompanyName, ContactName=@Contactname, ContactTitle=@ContactTitle, City=@City, Country=@Country, Phone=@Phone, Fax=@Fax 
                                                  where Id = @Id", connection))
            {
                command.Parameters.Add(new SqlParameter("@CompanyName", supplier.CompanyName));
                command.Parameters.Add(new SqlParameter("@ContactName", supplier.ContactName));
                command.Parameters.Add(new SqlParameter("@ContactTitle", supplier.ContactTitle));
                command.Parameters.Add(new SqlParameter("@City", supplier.City));
                command.Parameters.Add(new SqlParameter("@Country", supplier.Country));
                command.Parameters.Add(new SqlParameter("@Phone", supplier.Phone));
                command.Parameters.Add(new SqlParameter("@Fax", supplier.Fax));
                command.Parameters.Add(new SqlParameter("@Id", supplier.Id));
                command.ExecuteNonQuery();
            }
        }
    }
}
