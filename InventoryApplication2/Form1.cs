using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryApplication2.DataRepository;

namespace InventoryApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductRepository repository = new ProductRepository();
            var product = repository.Get(1);
            if (product != null)
            {
                textBox1.Text = product.ProductName;
            }
            else
            {
                textBox1.Text = "not available";
            }
        }
    }
}
