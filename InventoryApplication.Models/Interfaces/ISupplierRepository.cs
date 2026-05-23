using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryApplication.Models.Models;

namespace InventoryApplication.Models.Interfaces
{
    public interface ISupplierRepository
    {
        void Add(Supplier supplier);
        void Update(Supplier supplier);
        void Delete(int id);

        List<Supplier> GetAll();
        List<Supplier> GetAll(string companyName);
        Supplier Get(int supplierId);
    }
}
