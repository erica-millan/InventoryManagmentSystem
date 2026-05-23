using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryApplication.Models.Models;
using InventoryApplication.Models.Interfaces;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace InventoryApplication2.DataRepository
{
    public class ProductRepository : IProductRepository
    {
        private static ProductRepository instance;
        public static ProductRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductRepository();
                }
                return instance;
            }
        }
        public ProductRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ToString();
        }
        private readonly string _connectionString;
        public void Add(Product product)
        {
            using (var connection = new SqlConnection(_connectionString)) 
            using (var command = new SqlCommand("Insert into Product (ProductName, SupplierId, UnitPrice, Package, Stock, IsDiscontinued) " +
                                                "values (@ProductName, @SupplierId, @UnitPrice, @Package, @Stock, @IsDiscontinued)", connection)) 
            {
                command.Parameters.Add(new SqlParameter("@ProductName", product.ProductName));
                command.Parameters.Add(new SqlParameter("@SupplierId", product.SupplierId));
                command.Parameters.Add(new SqlParameter("@UnitPrice", product.UnitPrice));
                command.Parameters.Add(new SqlParameter("@Package", product.Package));
                command.Parameters.Add(new SqlParameter("@Stock", product.Stock));
                command.Parameters.Add(new SqlParameter("@IsDiscontinued", product.IsDiscontinued));
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("delete from Product where Id = @Id", connection))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlDataAdapter("select * from Product", connection))
            {
                DataTable dt = new DataTable();
                command.Fill(dt);
                dt.Rows.OfType<DataRow>().ToList().ForEach(p =>
                {
                    products.Add(new Product
                    {
                         Id = p.Field<int>("Id"), 
                         ProductName = p.Field<string>("ProductName"),
                         IsDiscontinued = p.Field<bool>("IsDiscontinued"),
                         Package = p.Field<string>("Package"),
                         Stock = p.Field<int>("Stock"),
                         UnitPrice = p.Field<decimal>("UnitPrice"),
                         SupplierId = p.Field<int>("SupplierId"),
                    });
                });
            }
            return products;
        }

        public List<Product> GetAll(string productName)
        {
            List<Product> products = new List<Product>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlDataAdapter("select * from Product where ProductName = @ProductName", connection))
            {
                DataTable dt = new DataTable();
                command.SelectCommand.Parameters.Add(new SqlParameter("@ProductName", productName));
                command.Fill(dt);
                dt.Rows.OfType<DataRow>().ToList().ForEach(p =>
                {
                    products.Add(new Product
                    {
                        Id = p.Field<int>("Id"),
                        ProductName = p.Field<string>("ProductName"),
                        IsDiscontinued = p.Field<bool>("IsDiscontinued"),
                        Package = p.Field<string>("Package"),
                        Stock = p.Field<int>("Stock"),
                        UnitPrice = p.Field<decimal>("UnitPrice"),
                        SupplierId = p.Field<int>("SupplierId"),
                    });
                });
            }
            return products;
        }

        public Product Get(int productId)
        {
            Product product = null;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlDataAdapter("select * from Product where Id = @Id", connection))
            {
                DataTable dt = new DataTable();
                command.SelectCommand.Parameters.Add(new SqlParameter("@Id", productId));
                command.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    var p = dt.Rows[0];
                    product = new Product
                    {
                        Id = p.Field<int>("Id"),
                        ProductName = p.Field<string>("ProductName"),
                        IsDiscontinued = p.Field<bool>("IsDiscontinued"),
                        Package = p.Field<string>("Package"),
                        Stock = p.Field<int>("Stock"),
                        UnitPrice = p.Field<decimal>("UnitPrice"),
                        SupplierId = p.Field<int>("SupplierId"),
                    };
                }
            }
            return product;
        }

        public void Update(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(@"Update Product set ProductName=@ProductName, SupplierId=@SupplierId, UnitPrice=@UnitPrice, Package=@Package, Stock=@Stock, IsDiscontinued=@IsDiscontinued 
                                                  where Id = @Id" , connection))
            {
                command.Parameters.Add(new SqlParameter("@ProductName", product.ProductName));
                command.Parameters.Add(new SqlParameter("@SupplierId", product.SupplierId));
                command.Parameters.Add(new SqlParameter("@UnitPrice", product.UnitPrice));
                command.Parameters.Add(new SqlParameter("@Package", product.Package));
                command.Parameters.Add(new SqlParameter("@Stock", product.Stock));
                command.Parameters.Add(new SqlParameter("@IsDiscontinued", product.IsDiscontinued));
                command.Parameters.Add(new SqlParameter("@Id", product.Id));
                command.ExecuteNonQuery();
            }
        }
    }
}
