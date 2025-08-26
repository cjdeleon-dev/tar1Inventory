using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccesses.SupplierAccess;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.SupplierService
{
    public class SupplierService : ISupplierService
    {
        ISupplierAccess isupaccess;

        public SupplierService()
        {
            this.isupaccess = new SupplierAccess();
        }

        public SupplierViewModel GetAllSuppliers()
        {
            return isupaccess.GetAllSuppliers();
        }

        public ProcessViewModel InsertSupplier(SupplierModel supm)
        {
            return isupaccess.InsertSupplier(supm);
        }

        public ProcessViewModel RemoveSupplier(int id)
        {
            return isupaccess.RemoveSupplier(id);
        }

        public ProcessViewModel UpdateSupplier(SupplierModel supm)
        {
            return isupaccess.UpdateSupplier(supm);
        }
    }
}