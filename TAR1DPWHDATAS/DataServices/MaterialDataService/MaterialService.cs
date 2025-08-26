using System;
using System.Collections.Generic;
using System.Text;
using TAR1DPWHDATA.DataAccesses.MaterialDataAccess;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.MaterialDataService
{
    public class MaterialService : IMaterialService
    {
        IMaterialAccess imaccess;

        public MaterialService()
        {
            imaccess = new MaterialAccess();
        }

        public MaterialViewModel GetAllMaterials()
        {
            return imaccess.GetAllMaterials();
        }

        public MaterialCUDViewModel InsertMaterial(MaterialModel material)
        {
            return imaccess.InsertMaterial(material);
        }

        public MaterialCUDViewModel RemoveMaterial(int id)
        {
            return imaccess.RemoveMaterial(id);
        }

        public MaterialCUDViewModel UpdateMaterial(MaterialModel material)
        {
            return imaccess.UpdateMaterial(material);
        }
    }
}
