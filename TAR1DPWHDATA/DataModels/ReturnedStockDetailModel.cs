using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.DataModels
{
    public class ReturnedStockDetailModel
    {
        public int Id { get; set; }
        public int MCRTNo { get; set; }
        public int? MCTNo { get; set; }
        public bool IsSalvage { get; set; }
        public int MaterialId { get; set; }
        public string Material { get; set; }
        public string Stock { get; set; }
        public string SerialNo { get; set; }
        public int YearId { get; set; }
        public string YearsValue { get; set; }
        public double RateAmount { get; set; }
        public int Quantity { get; set; }
        public double TotalAmount { get; set; }
    }
}