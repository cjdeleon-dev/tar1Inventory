using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.SupplierService
{
    public interface ISupplierService
    {
        SupplierViewModel GetAllSuppliers();
        ProcessViewModel InsertSupplier(SupplierModel supm);
        ProcessViewModel UpdateSupplier(SupplierModel supm);
        ProcessViewModel RemoveSupplier(int id);
    }
}
