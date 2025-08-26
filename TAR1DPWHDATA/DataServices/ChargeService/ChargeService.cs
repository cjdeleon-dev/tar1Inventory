using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccesses.ChargeAccess;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.ChargeService
{
    public class ChargeService : IChargeService
    {
        IChargeAccess ica;

        public ChargeService()
        {
            this.ica = new ChargeAccess();
        }

        public ChargeMaterialHeaderViewModel GetAllChargedMaterialHeaders()
        {
            return ica.GetAllChargedMaterialHeaders();
        }

        public ChargeMaterialDetailViewModel GetChargedMaterialDetailsByHeaderId(int cmhdrid)
        {
            return ica.GetChargedMaterialDetailsByHeaderId(cmhdrid);
        }

        public ChargeMaterialHeaderModel GetCurrentCMHIdByUserId(int id)
        {
            return ica.GetCurrentCMHIdByUserId(id);
        }

        public List<MCTExportModel> GetMCTExportModels(string dateFrom, string dateTo)
        {
            return ica.GetMCTExportModels(dateFrom, dateTo);
        }

        public string GetUnitAndOnHandByMaterialId(int matid)
        {
            return ica.GetUnitAndOnHandByMaterialId(matid);
        }

        public ProcessViewModel InsertChargedMaterialDetails(List<ChargeMaterialDetailModel> lstcmdm)
        {
            return ica.InsertChargedMaterialDetails(lstcmdm);
        }

        public ProcessViewModel InsertChargedMaterialHeader(ChargeMaterialHeaderModel cmhm)
        {
            return ica.InsertChargedMaterialHeader(cmhm);
        }

    }
}