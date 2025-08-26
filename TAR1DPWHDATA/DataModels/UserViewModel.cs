using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.DataModels
{
    public class UserViewModel
    {
        public List<UserModel> Users { get; set; }
        public bool IsError { get; set; }
        public string ProcessMessage { get; set; }
    }
}