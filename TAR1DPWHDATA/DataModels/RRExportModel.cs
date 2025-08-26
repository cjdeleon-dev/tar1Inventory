using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.DataModels
{
    public class RRExportModel
    {
        public int RRNo { get; set; }
        public string ReceivedDate { get; set; }
        public string PreparedBy { get; set; }
        public double ReceivedTotalCost { get; set; }
        public string Supplier { get; set; }
        public string PONos { get; set; }
        public string SINos { get; set; }
        public string DRNos { get; set; }
        public string DeliveryDate { get; set; }
        public string Remark { get; set; }
        public string Material { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public double UnitCost { get; set; }
        public double TotalCost { get; set; }
        public double InventorialCost { get; set; }
        public double VAT { get; set; }
        public int OnHand { get; set; }
        public int BalanceQty { get; set; }
        public string BalanceRemark { get; set; }
    }
}