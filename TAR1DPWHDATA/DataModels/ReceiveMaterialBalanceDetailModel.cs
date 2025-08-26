using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.DataModels
{
    public class ReceiveMaterialBalanceDetailModel
    {
        public int Id { get; set; }
        public string Material { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string Remark { get; set; }
    }
}