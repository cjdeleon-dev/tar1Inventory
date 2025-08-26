using System;
using System.Collections.Generic;
using System.Text;

namespace TAR1DPWHDATA.DataModels
{
    public class MaterialModel
    {
        public int Id { get; set; }
        public string Material { get; set; }
        public string Description { get; set; }
        public double OnHand { get; set; }
    }
}
