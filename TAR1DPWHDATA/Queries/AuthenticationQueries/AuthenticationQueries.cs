using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.Queries.AuthenticationQueries
{
    public class AuthenticationQueries
    {
        public static readonly string sqlGetUserByUserCredentials = "select users.id,firstname,middleinitial,lastname,positions.description [position],isactive " +
                                                                    "from users left join positions on users.positionid=positions.id " +
                                                                    "where username=@username and password=@password;";
        public static readonly string sqlGetRolesByUserName = "select u.roleid,r.role from users u inner join roles r on u.roleid=r.id where u.username=@uname;";
    }
}