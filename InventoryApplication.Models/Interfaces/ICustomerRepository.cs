using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryApplication.Models.Models;
//icustomerrepository is a parent, and customerrepository is a child of this parent class. 
namespace InventoryApplication.Models.Interfaces
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int id);

        List<Customer> GetAll();
        List<Customer> GetAll(string firstName);
        Customer Get(int customerId);
    }
}
