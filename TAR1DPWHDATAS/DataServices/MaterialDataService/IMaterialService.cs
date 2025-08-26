using System;
using System.Collections.Generic;
using System.Text;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.MaterialDataService
{
    public interface IMaterialService
    {
        MaterialViewModel GetAllMaterials();
        MaterialCUDViewModel InsertMaterial(MaterialModel material);
        MaterialCUDViewModel UpdateMaterial(MaterialModel material);
        MaterialCUDViewModel RemoveMaterial(int id);
    }
}
