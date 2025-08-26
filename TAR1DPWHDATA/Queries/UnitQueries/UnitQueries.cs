using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.Queries.UnitQueries
{
    public class UnitQueries
    {
        public readonly static string sqlGetAllUnits = "select id, code, description from units where isnull(isDeleted,0)=0;";
        public readonly static string sqlInsertUnit = "insert into units(code,description) values(@code,@description);";
        public readonly static string sqlUpdateUnit = "update units set code=@code, description=@description where id=@id;";
        public readonly static string sqlRemoveUnit = "update units set isDeleted=1 where id=@id;";
    }
}