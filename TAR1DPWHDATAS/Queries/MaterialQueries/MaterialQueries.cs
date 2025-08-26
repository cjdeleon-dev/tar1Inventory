using System;
using System.Collections.Generic;
using System.Text;

namespace TAR1DPWHDATA.Queries.MaterialQueries
{
    public class MaterialQueries
    {
        public readonly static string sqlGetAllMaterials = "select id, material, description from materials where isnull(isDeleted,0)=0;";
        public readonly static string sqlInsertMaterial = "insert into materials(material,description) values(@material,@description);";
        public readonly static string sqlUpdateMaterial = "update materials set material=@material, description=@description where id=@id;";
        public readonly static string sqlRemoveMaterial = "update materials set isDeleted=1 where id=@id;";
    }
}
