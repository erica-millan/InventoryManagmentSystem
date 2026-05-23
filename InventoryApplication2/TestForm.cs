using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryApplication.Models.Models;
using InventoryApplication2.Business_Logic;
using InventoryApplication2.DataRepository;

namespace InventoryApplication2
{
    public partial class TestForm : Form
    {
        ProductRepository productRepository = new ProductRepository();
        public TestForm()
        {
            InitializeComponent();
        }

        BindingList<Supplier> supplyBindings;
        private void TestForm_Load(object sender, EventArgs e)
        {
            SupplierRepository supplierFactory = new SupplierRepository();
            var suppliers = supplierFactory.GetAll();
            supplyBindings = new BindingList<Supplier>(suppliers);
            comboBox1.DisplayMember = "CompanyName";
            comboBox1.DataSource = supplyBindings;
            dGOrderItems.AutoGenerateColumns = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var supplier = (Supplier)comboBox1.SelectedItem;
            if (supplier == null)
            {
                MessageBox.Show("Supplier not selected");
                return;
            }
            
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Product name was not given.");
                return;
            }

            decimal unitPrice = 0.00M;
            if (string.IsNullOrEmpty(textBox2.Text) || !decimal.TryParse(textBox2.Text, out unitPrice))
            {
                MessageBox.Show("Unit price is invalid.");
                return;
            }

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Package was not given.");
                return;
            }

            int stock = 0;
            if (string.IsNullOrEmpty(textBox4.Text) || !int.TryParse(textBox4.Text, out stock))
            {
                MessageBox.Show("Stock is invalid.");
                return;
            }

            Product product = new Product()
            {
                ProductName = textBox1.Text,
                SupplierId = supplier.Id,
                UnitPrice = unitPrice,
                Package = textBox3.Text,
                Stock = stock,
                IsDiscontinued = checkBox1.Checked

            };

            productRepository.Add(product);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerId.Text))
            {
                MessageBox.Show("Customer Id was not input.");
                return;
            }

            if (!int.TryParse(txtCustomerId.Text,out int cid))
            {
                MessageBox.Show("Invalid customer Id");
                return;
            }

            var customer = CustomerFactory.GetCustomer(cid);
            txtFirstName.Text = customer.FirstName;
            txtLastName.Text = customer.LastName;
            dGOrders.DataSource = new BindingList<Order>(customer.Orders);

        }

        private void dGOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dGOrders.SelectedRows.Count > 0)
            {
                var selectedItem = (Order)dGOrders.SelectedRows[0].DataBoundItem;
                int orderId = selectedItem.Id;
                var items = OrderFactory.GetOrderProducts(orderId);
                dGOrderItems.DataSource = new BindingList<OrderProducts>(items);
            }
        }
    }
}
