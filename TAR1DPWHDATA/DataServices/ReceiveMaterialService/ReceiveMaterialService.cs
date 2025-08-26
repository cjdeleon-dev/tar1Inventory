using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccesses.ReceiveMaterialAccess;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.ReceiveMaterialService
{
    public class ReceiveMaterialService : IReceiveMaterialService
    {
        IReceiveMaterialAccess irma;

        public ReceiveMaterialService()
        {
            this.irma = new ReceiveMaterialAccess();
        }

        public EmployeeViewModel GetAllEmployees()
        {
            return irma.GetAllEmployees();
        }

        public ReceiveMaterialHeaderViewModel GetAllReceivedMaterialHeaders()
        {
            return irma.GetAllReceivedMaterialHeaders();
        }

        public ReceiveMaterialHeaderViewModel GetAllReceivedNonStockMaterialHeader()
        {
            return irma.GetAllReceivedNonStockMaterialHeader();
        }

        public ReceiveMaterialHeaderModel GetCurrentRMIdByUserId(int id)
        {
            return irma.GetCurrentRMIdByUserId(id);
        }

        public ReceiveMaterialHeaderModel GetCurrentRMNSIdByUserId()
        {
            return irma.GetCurrentRMNSIdByUserId();
        }

        public ReceiveMaterialBalanceDetailViewModel GetRecMaterialBalanceDetailByHeaderid(int rmhdrid)
        {
            return irma.GetRecMaterialBalanceDetailByHeaderid(rmhdrid);
        }

        public ReceiveMaterialDetailViewModel GetRecMaterialDetailByHeaderId(int rmhdrid)
        {
            return irma.GetRecMaterialDetailByHeaderId(rmhdrid);
        }

        public ReceiveMaterialDetailViewModel GetRecMaterialNonStockDetailByHeaderId(int rmnshdrid)
        {
            return irma.GetRecMaterialNonStockDetailByHeaderId(rmnshdrid);
        }

        public List<RRExportModel> GetRRExportData(string dateFrom, string dateTo)
        {
            return irma.GetRRExportData(dateFrom, dateTo);
        }

        public int GetUnitIdByMaterialId(int materialId)
        {
            return irma.GetUnitIdByMaterialId((int)materialId);
        }

        public ProcessViewModel InsertReceivedMaterialDetail(List<ReceiveMaterialDetailModel> lstrmdm)
        {
            return irma.InsertReceivedMaterialDetail(lstrmdm);
        }

        public ProcessViewModel InsertReceivedMaterialHeader(ReceiveMaterialHeaderModel rmhm)
        {
            return irma.InsertReceivedMaterialHeader(rmhm);
        }

        public ProcessViewModel InsertReceivedMaterialNonStockDetail(ReceiveMaterialDetailModel rmnsdm)
        {
            return irma.InsertReceivedMaterialNonStockDetail(rmnsdm);
        }

        public ProcessViewModel InsertReceivedNonStockMaterialHeader(ReceiveMaterialHeaderModel rmnshm)
        {
            return irma.InsertReceivedNonStockMaterialHeader(rmnshm);
        }
    }
}