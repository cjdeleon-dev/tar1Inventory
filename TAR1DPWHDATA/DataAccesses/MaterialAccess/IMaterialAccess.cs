using System;
using System.Collections.Generic;
using System.Text;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataAccesses.MaterialDataAccess
{
    public interface IMaterialAccess
    {
        MaterialViewModel GetAllMaterials();
        MaterialViewModel GetAllMaterialsForMCT();
        ProcessViewModel InsertMaterial(MaterialModel material);
        ProcessViewModel UpdateMaterial(MaterialModel material);
        ProcessViewModel RemoveMaterial(int id);

        MaterialViewModel GetAllNonStocks();
        ProcessViewModel InsertNonStock(MaterialModel material);
        ProcessViewModel UpdateNonStock(MaterialModel material);
        ProcessViewModel RemoveNonStock(int id);
    }
}
