using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryApplication.Models.Models;

namespace InventoryApplication.Models.Interfaces
{
    // shopwing you all of the methods and lists that are in the repository
    public interface IProductRepository
    {
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);

        List<Product> GetAll();
        List<Product> GetAll(string city);
        Product Get(int firstName);
    }
}
