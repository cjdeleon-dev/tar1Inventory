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

        public MaterialViewModel GetAllMaterialsForMCT()
        {
            return imaccess.GetAllMaterialsForMCT();
        }

        public MaterialViewModel GetAllNonStocks()
        {
            return imaccess.GetAllNonStocks();
        }

        public ProcessViewModel InsertMaterial(MaterialModel material)
        {
            return imaccess.InsertMaterial(material);
        }

        public ProcessViewModel InsertNonStock(MaterialModel material)
        {
            return imaccess.InsertNonStock(material);
        }

        public ProcessViewModel RemoveMaterial(int id)
        {
            return imaccess.RemoveMaterial(id);
        }

        public ProcessViewModel RemoveNonStock(int id)
        {
            return imaccess.RemoveNonStock(id);
        }

        public ProcessViewModel UpdateMaterial(MaterialModel material)
        {
            return imaccess.UpdateMaterial(material);
        }

        public ProcessViewModel UpdateNonStock(MaterialModel material)
        {
            return imaccess.UpdateNonStock(material);
        }
    }
}
