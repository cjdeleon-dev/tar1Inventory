using System;
using System.Collections.Generic;
using System.Text;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataAccesses.MaterialDataAccess
{
    public interface IMaterialAccess
    {
        MaterialViewModel GetAllMaterials();
        MaterialCUDViewModel InsertMaterial(MaterialModel material);
        MaterialCUDViewModel UpdateMaterial(MaterialModel material);
        MaterialCUDViewModel RemoveMaterial(int id);
    }
}
