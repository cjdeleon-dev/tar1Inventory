using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.Queries.RoleQueries
{
    public class RoleQueries
    {
        public static readonly string sqlGetAllRoles = "select id, code, role from roles where isnull(isdeleted,0)=0;";
        public static readonly string sqlInsertRole = "insert into roles(code,role) values(@code,@role);";
        public static readonly string sqlUpdateRole = "update roles set code=@code, role=@role where id=@id;";
        public static readonly string sqlDeleteRole = "update roles set isdeleted=@isdeleted where id=@id;";
    }
}