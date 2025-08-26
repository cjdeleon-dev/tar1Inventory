using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.DataModels
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public int PositionId { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }
        public byte[] UserPic { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

        public int RoleId { get; set; }
        public string Role { get; set; }
    }
}