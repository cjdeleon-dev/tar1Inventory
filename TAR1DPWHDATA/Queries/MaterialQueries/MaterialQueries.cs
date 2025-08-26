using System;
using System.Collections.Generic;
using System.Text;

namespace TAR1DPWHDATA.Queries.MaterialQueries
{
    public class MaterialQueries
    {
        public readonly static string sqlGetAllMaterials = "select id, material, description, OnHand from materials where isnull(isDeleted,0)=0 order by material;";
        public readonly static string sqlGetAllMaterialsForMCT = "select id, material, description, sum(onhand) onhand " +
                                                                 "from " +
                                                                 "( " +
                                                                 "  select dtl.materialid [id],mat.material,mat.description,sum(dtl.OnHand) onhand " +
                                                                 "  from ReceivedMaterialDetails dtl " +
                                                                 "  inner join Materials mat " +
                                                                 "  on dtl.MaterialId=mat.Id " +
                                                                 "  group by dtl.MaterialId, mat.Material, mat.Description " +
                                                                 "  having SUM(dtl.OnHand)>0 " +
                                                                 "  UNION ALL " +
                                                                 "  select dtl.materialid [id],mat.material,mat.description,sum(dtl.OnHand) onhand " +
                                                                 "  from ReturnedChargedMaterialDetails dtl " +
                                                                 "  inner join Materials mat " +
                                                                 "  on dtl.MaterialId=mat.Id  "+
                                                                 "  group by dtl.MaterialId, mat.Material, mat.Description " +
                                                                 "  having SUM(dtl.OnHand)>0 " +
                                                                 ") src " +
                                                                 "group by id, Material,Description " +
                                                                 "order by material, Description;";

        public readonly static string sqlInsertMaterial = "insert into materials(material,description) values(@material,@description);";
        public readonly static string sqlUpdateMaterial = "update materials set material=@material, description=@description where id=@id;";
        public readonly static string sqlRemoveMaterial = "update materials set isDeleted=1 where id=@id;";



        public readonly static string sqlGetAllNonStocks = "select id, description, onhand from nonstocks where isnull(isDeleted,0)=0;";
        public readonly static string sqlInsertNonStock = "insert into nonstocks(material,description) values(@material,@description);";
        public readonly static string sqlUpdateNonStock = "update nonstocks set material=@material, description=@description where id=@id;";
        public readonly static string sqlRemoveNonStock = "update nonstocks set isDeleted=1 where id=@id;";
    }
}
