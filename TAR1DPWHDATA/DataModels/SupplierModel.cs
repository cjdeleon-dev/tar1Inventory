using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.DataModels
{
    public class SupplierModel
    {
        public int Id { get; set; }
        public string Supplier { get; set; }
        public string Address { get; set; }
        public int CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public int UpdatedById { get; set; }
        public string UpdatedBy { get; set; }
    }
}