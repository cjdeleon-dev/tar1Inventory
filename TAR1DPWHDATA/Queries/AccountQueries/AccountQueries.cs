using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.Queries.AccountQueries
{
    public class AccountQueries
    {
        public static readonly string sqlDeletePhoto = "update users set userpic=null where id=@id;";
        public static readonly string sqlUpdatePhoto = "update users set userpic=@userpic where id=@id;";
        public static readonly string sqlGetPasswordByID = "select id,password from users where id=@id;";
        public static readonly string sqlUpdatePasswordById = "update users set password=@password where id=@id;";
    }
}