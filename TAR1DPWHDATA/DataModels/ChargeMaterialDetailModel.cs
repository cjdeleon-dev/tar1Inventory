using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.DataModels
{
    public class ChargeMaterialDetailModel
    {
        public int Id { get; set; }
        public int ChargeMaterialHeaderId { get; set; }
        public int MaterialId { get; set; }
        public string Material { get; set; }
        public string SerialNo { get; set; }
        public int Quantity { get; set; }
        public int UnitId { get; set; }
        public string Unit { get; set; }
        public int JOWOMOId { get; set; }
        public string JOWOMOCode { get; set; }
        public string JOWOMONumber { get; set; }
    }
}