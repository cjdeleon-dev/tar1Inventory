using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.DataModels
{
    public class UserLoginModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public bool IsActive { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        public List<RoleModel> Roles { get; set; }
    }
}