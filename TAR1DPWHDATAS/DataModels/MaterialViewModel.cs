using System;
using System.Collections.Generic;
using System.Text;

namespace TAR1DPWHDATA.DataModels
{
    public class MaterialViewModel
    {
        public List<MaterialModel> Materials { get; set; }
        public bool IsError { get; set; }
        public string ProcessMessage { get; set; }
    }
}
