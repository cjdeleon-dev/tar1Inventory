using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.DataModels
{
    public class RoleViewModel
    {
        public List<RoleModel> Roles { get; set; }
        public bool IsError { get; set; }
        public string ProcessMessage { get; set; }
    }
}