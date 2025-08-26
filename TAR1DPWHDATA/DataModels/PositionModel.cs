using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.DataModels
{
    public class PositionModel
    {
        public int Id { get; set; }
        public string Code{ get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
    }
}