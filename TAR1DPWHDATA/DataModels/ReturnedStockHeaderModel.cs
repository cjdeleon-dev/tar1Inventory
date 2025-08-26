using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.DataModels
{
    public class ReturnedStockHeaderModel
    {
        public int Id { get; set; }
        public string ReturnedDate { get; set; }
        public bool IsConsumer { get; set; }
        public int? ReturnedById { get; set; }
        public string ReturnedBy { get; set; }
        public int CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public string Remarks { get; set; }
        public string EntryDate { get; set; }
    }
}