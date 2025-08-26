using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.DataModels
{
    public class ReceiveMaterialDetailViewModel
    {
        public List<ReceiveMaterialDetailModel> ReceivedMaterialDetails { get; set; }
        public bool IsError { get; set; }
        public string ProcessMessage { get; set; }
    }
}