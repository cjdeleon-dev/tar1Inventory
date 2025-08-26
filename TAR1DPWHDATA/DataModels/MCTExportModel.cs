using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.DataModels
{
    public class MCTExportModel
    {
        public int MaterialId { get; set; }
        public string StockName { get; set; }
        public string StockDescription { get; set; }
        public string SerialNo { get; set; }
        public string PostedDate { get; set; }
        public int MCTNo { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public double UnitCost { get; set; }
        public double TotalCost { get; set; }
        public string WOCode { get; set; }
        public string WOAccount { get; set; }
        public string WONumber { get; set; }
        public string Project { get; set; }
        public string ProjectAddress { get; set; }

    }
}