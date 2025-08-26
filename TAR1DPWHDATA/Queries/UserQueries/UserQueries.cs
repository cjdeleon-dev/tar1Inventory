using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.Queries.UserQueries
{
    public class UserQueries
    {
        public readonly static string sqlGetAllUsers = "select usr.id, firstname, middleinitial,lastname,usr.positionid,post.description[position],address,username,password,usr.userpic,usr.isactive,usr.roleid " +
                                                       "from users usr left join positions post on usr.positionid=post.id where isnull(usr.isactive,0)=1;";
        public readonly static string sqlInsertUser = "insert into users(firstname,middleinitial,lastname,positionid,address,username,password,isactive,roleid) " +
                                                      "values(@firstname,@middleinitial,@lastname,@positionid,@address,@username,@password,1,@roleid);";
        public readonly static string sqlUpdateUser = "update users set firstname=@firstname,middleinitial=@middleinitial,lastname=@lastname," +
                                                      "isactive=@isactive,positionid=@positionid,address=@address,roleid=@roleid where id=@id;";
        public readonly static string sqlDeactivateUser = "update users set isActive=0 where id=@id;";
        public readonly static string sqlChangeUserCredentials = "update users set username=@username, password=@password where id=@id;";
    }
}