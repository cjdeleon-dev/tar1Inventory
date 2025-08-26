using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.Queries.DepartmentQueries
{
    public class DepartmentQueries
    {
        public readonly static string sqlGetAllDepartments = "select id, code, description from departments where isnull(isDeleted,0)=0;";
        public readonly static string sqlInsertDepartment = "insert into departments(code,description) values(@code,@description);";
        public readonly static string sqlUpdateDepartment = "update departments set code=@code, description=@description where id=@id;";
        public readonly static string sqlRemoveDepartment = "update departments set isDeleted=1 where id=@id;";
    }
}