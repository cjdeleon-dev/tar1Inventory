using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.ReceiveMaterialService
{
    public interface IReceiveMaterialService
    {
        ReceiveMaterialHeaderViewModel GetAllReceivedMaterialHeaders();
        ProcessViewModel InsertReceivedMaterialHeader(ReceiveMaterialHeaderModel rmhm);
        ReceiveMaterialHeaderModel GetCurrentRMIdByUserId(int id);
        ProcessViewModel InsertReceivedMaterialDetail(List<ReceiveMaterialDetailModel> lstrmdm);
        ReceiveMaterialDetailViewModel GetRecMaterialDetailByHeaderId(int rmhdrid);
        ReceiveMaterialBalanceDetailViewModel GetRecMaterialBalanceDetailByHeaderid(int rmhdrid);

        EmployeeViewModel GetAllEmployees();

        ReceiveMaterialHeaderViewModel GetAllReceivedNonStockMaterialHeader();
        ProcessViewModel InsertReceivedNonStockMaterialHeader(ReceiveMaterialHeaderModel rmnshm);
        ReceiveMaterialHeaderModel GetCurrentRMNSIdByUserId();
        ProcessViewModel InsertReceivedMaterialNonStockDetail(ReceiveMaterialDetailModel rmnsdm);
        ReceiveMaterialDetailViewModel GetRecMaterialNonStockDetailByHeaderId(int rmnshdrid);

        List<RRExportModel> GetRRExportData(string dateFrom, string dateTo);

        int GetUnitIdByMaterialId(int materialId);
    }
}
