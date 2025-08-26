using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.ChargeService
{
    public interface IChargeService
    {
        ChargeMaterialHeaderViewModel GetAllChargedMaterialHeaders();
        ProcessViewModel InsertChargedMaterialHeader(ChargeMaterialHeaderModel cmhm);
        ChargeMaterialHeaderModel GetCurrentCMHIdByUserId(int id);
        ProcessViewModel InsertChargedMaterialDetails(List<ChargeMaterialDetailModel> lstcmdm);
        ChargeMaterialDetailViewModel GetChargedMaterialDetailsByHeaderId(int cmhdrid);
        string GetUnitAndOnHandByMaterialId(int matid);
        List<MCTExportModel> GetMCTExportModels(string dateFrom, string dateTo);
    }
}
