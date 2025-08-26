using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.Queries.PositionQueries
{
    public class PositionQueries
    {
        public readonly static string sqlGetAllPositions = "select post.id, post.code, post.description,isnull(post.DepartmentId,0)departmentid, isnull(dept.description,'') [department] " +
                                                           "from positions post left join departments dept " +
                                                           "on post.departmentid=dept.id where isnull(post.isDeleted,0)=0;";
        public readonly static string sqlInsertPosition = "insert into positions(code,description,departmentid) values(@code,@description,@departmentid);";
        public readonly static string sqlUpdatePosition = "update positions set code=@code, description=@description, departmentid=@departmentid where id=@id;";
        public readonly static string sqlRemovePosition = "update positions set isDeleted=1 where id=@id;";
    }
}