using Microsoft.Reporting.WebForms;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.DataServices.ChargeService;
using TAR1DPWHDATA.DataServices.MaterialDataService;
using TAR1DPWHDATA.DataServices.ReceiveMaterialService;
using TAR1DPWHDATA.DataServices.SupplierService;
using TAR1DPWHDATA.DataServices.UnitService;
using TAR1DPWHDATA.DataServices.UserService;
using TAR1DPWHDATA.Globals;

namespace TAR1DPWHWEB.Controllers
{
    [Authorize(Roles = "Administrator, Employee")]
    public class ReceiveController : Controller
    {
        IReceiveMaterialService irms;
        IUserService ius;
        IMaterialService ims;
        IUnitService iuns;
        ISupplierService isups;
        // GET: Receive
        
        //public ActionResult ReceivedMaterials(string search, int? i)
        public ActionResult ReceivedMaterials()
        {
            
            if (!GlobalVars.isAdmin)
                ViewBag.Message = "Regular";
            else
                ViewBag.Message = "Admin";

            //if (lstrmh != null)
            //{
            //    if (search == null)
            //        return View(lstrmh.ToPagedList(i ?? 1, 10));
            //    else
            //        return View(lstrmh.Where(x => x.PreparedBy.ToLower().StartsWith(search.ToLower())).ToPagedList(i ?? 1, 10));
            //}
            //else
            //{
            //    return View(lstrmh);
            //}
            return View();
        }

        public JsonResult GetAllRRHeaders()
        {
            irms = new ReceiveMaterialService();

            List<ReceiveMaterialHeaderModel> lstrmh = new List<ReceiveMaterialHeaderModel>();

            lstrmh = irms.GetAllReceivedMaterialHeaders().ReceivedMaterialHeaders;

            var jsonResult = Json(new { data = lstrmh }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult GetLoggedUserName()
        {
            ius = new UserService();
            UserModel um = new UserModel();

            int loggedid = Convert.ToInt32(System.Web.HttpContext.Current.Session["Id"]);

            //um = ius.GetAllUsers().Users.Find(x => x.Id.Equals(GlobalVars.IdLogged));
            um = ius.GetAllUsers().Users.Find(x => x.Id.Equals(loggedid));

            return Json(um, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMaterials()
        {
            ims = new MaterialService();
            List<MaterialModel> lstmm = new List<MaterialModel>();
            lstmm = ims.GetAllMaterials().Materials;

            return Json(lstmm, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnits()
        {
            iuns = new UnitService();
            List<UnitModel> lstunit = new List<UnitModel>();
            lstunit = iuns.GetAllUnits().Units;

            return Json(lstunit, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddReceivedHeader(ReceiveMaterialHeaderModel rmhm)
        {
            irms = new ReceiveMaterialService();
            int loggedid = Convert.ToInt32(System.Web.HttpContext.Current.Session["Id"]);
            rmhm.PreparedById = loggedid;
            return Json(irms.InsertReceivedMaterialHeader(rmhm), JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetLoggedUserMaxRMId()
        {
            irms = new ReceiveMaterialService();
            ReceiveMaterialHeaderModel rm = new ReceiveMaterialHeaderModel();
            int loggedid = Convert.ToInt32(System.Web.HttpContext.Current.Session["Id"]);
            rm = irms.GetCurrentRMIdByUserId(loggedid);
            return Json(rm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddReceiveMaterialDetail(List<ReceiveMaterialDetailModel> lstrmdm)
        {
            irms = new ReceiveMaterialService();
            return Json(irms.InsertReceivedMaterialDetail(lstrmdm), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRecMaterialDetailByHeaderId(ReceiveMaterialDetailModel rmdm)
        {
            irms = new ReceiveMaterialService();
            ReceiveMaterialDetailViewModel rmdvm = new ReceiveMaterialDetailViewModel();
            rmdvm = irms.GetRecMaterialDetailByHeaderId(rmdm.ReceivedMaterialHeaderId);
            //List<ReceiveMaterialDetailModel> lstrmdm = new List<ReceiveMaterialDetailModel>();
            //lstrmdm = irms.GetRecMaterialDetailByHeaderId(rmdm.ReceivedMaterialHeaderId).ReceivedMaterialDetails;
            return Json(rmdvm, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployees(ReceiveMaterialDetailModel rmdm)
        {
            irms = new ReceiveMaterialService();
            EmployeeViewModel evm = new EmployeeViewModel();
            evm = irms.GetAllEmployees();
            //List<ReceiveMaterialDetailModel> lstrmdm = new List<ReceiveMaterialDetailModel>();
            //lstrmdm = irms.GetRecMaterialDetailByHeaderId(rmdm.ReceivedMaterialHeaderId).ReceivedMaterialDetails;
            return Json(evm.Employees, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSuppliers()
        {
            isups = new SupplierService();
            SupplierViewModel svm = new SupplierViewModel();
            svm = isups.GetAllSuppliers();
            //List<ReceiveMaterialDetailModel> lstrmdm = new List<ReceiveMaterialDetailModel>();
            //lstrmdm = irms.GetRecMaterialDetailByHeaderId(rmdm.ReceivedMaterialHeaderId).ReceivedMaterialDetails;
            return Json(svm.Suppliers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RRReportView(int rptid)
        {
            LocalReport lr = new LocalReport();
            string p = Path.Combine(Server.MapPath("/Reports"), "rptReceivedMaterial.rdlc");
            lr.ReportPath = p;

            DataSet ds = new DataSet();

            //header
            ReceiveMaterialHeaderModel rhm = new ReceiveMaterialHeaderModel();
            irms = new ReceiveMaterialService();
            rhm = irms.GetAllReceivedMaterialHeaders().ReceivedMaterialHeaders.Find(x => x.Id.Equals(rptid));
            DataTable dtHeader = new DataTable();

            dtHeader = ModelToDataTable(rhm);
            ds.Tables.Add(dtHeader);

            //details
            irms = new ReceiveMaterialService();
            List<ReceiveMaterialDetailModel> lstrmdm = new List<ReceiveMaterialDetailModel>();
            lstrmdm = irms.GetRecMaterialDetailByHeaderId(rptid).ReceivedMaterialDetails;
            DataTable dtDetails = new DataTable();

            dtDetails = ListRMDMToDataTable(lstrmdm);
            ds.Tables.Add(dtDetails);

            //balance
            irms = new ReceiveMaterialService();
            List<ReceiveMaterialBalanceDetailModel> lstrmbdm = new List<ReceiveMaterialBalanceDetailModel>();
            lstrmbdm = irms.GetRecMaterialBalanceDetailByHeaderid(rptid).BalanceMaterials;
            DataTable dtNotes = new DataTable();

            dtNotes = ListRMBDMToDataTable(lstrmbdm);
            ds.Tables.Add(dtNotes);

            //ReportDataSource for Header
            ReportDataSource rdhdr = new ReportDataSource("dsRRStockHeader", ds.Tables["Table1"]);
            //ReportDataSource for Details
            ReportDataSource rddtl = new ReportDataSource("dsRRStockDetails", ds.Tables["Table2"]);
            //ReportDataSource for Notes
            ReportDataSource rdnotes = new ReportDataSource("dsRRStockNote", ds.Tables["Table3"]);

            lr.DataSources.Add(rdhdr);//Header
            lr.DataSources.Add(rddtl);//Details
            lr.DataSources.Add(rdnotes);//Material Balances

            string mt, enc, f;
            string[] s;
            Warning[] w;

            //Rendering
            byte[] b = lr.Render("PDF", null, out mt, out enc, out f, out s, out w);

            return File(b, mt);
        }

        public JsonResult FetchDateByDateRange(string datefr, string dateto)
        {
            irms = new ReceiveMaterialService();
            List<RRExportModel> lst = irms.GetRRExportData(datefr, dateto);

            var jsonResult = Json(new { data = lst }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult GetUnitByMatarialId(int materialid)
        {
            irms = new ReceiveMaterialService();
            return Json(new { unitid = irms.GetUnitIdByMaterialId(materialid) }, JsonRequestBehavior.AllowGet);
        }


        //Non-Stock

        public ActionResult ReceivedNonStockMaterials(string search, int? i)
        {
            irms = new ReceiveMaterialService();

            if (!GlobalVars.isAdmin)
                ViewBag.Message = "Regular";
            else
                ViewBag.Message = "Admin";

            List<ReceiveMaterialHeaderModel> lstrmh = new List<ReceiveMaterialHeaderModel>();

            lstrmh = irms.GetAllReceivedNonStockMaterialHeader().ReceivedMaterialHeaders;

            if (lstrmh != null)
            {
                if (search == null)
                    return View(lstrmh.ToPagedList(i ?? 1, 10));
                else
                    return View(lstrmh.Where(x => x.PreparedBy.ToLower().StartsWith(search.ToLower())).ToPagedList(i ?? 1, 10));
            }
            else
            {
                return View(lstrmh);
            }
        }

        [HttpPost]
        public JsonResult AddReceivedNonStockHeader(ReceiveMaterialHeaderModel rmhm)
        {
            irms = new ReceiveMaterialService();
            return Json(irms.InsertReceivedNonStockMaterialHeader(rmhm), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLoggedUserMaxRMNSId()
        {
            irms = new ReceiveMaterialService();
            ReceiveMaterialHeaderModel rm = new ReceiveMaterialHeaderModel();
            rm = irms.GetCurrentRMNSIdByUserId();
            return Json(rm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddReceiveNonStockMaterialDetail(ReceiveMaterialDetailModel rmdm)
        {
            irms = new ReceiveMaterialService();
            return Json(irms.InsertReceivedMaterialNonStockDetail(rmdm), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRecNonStockMaterialDetailByHeaderId(ReceiveMaterialDetailModel rmdm)
        {
            irms = new ReceiveMaterialService();
            ReceiveMaterialDetailViewModel rmdvm = new ReceiveMaterialDetailViewModel();
            rmdvm = irms.GetRecMaterialNonStockDetailByHeaderId(rmdm.ReceivedMaterialHeaderId);
            //List<ReceiveMaterialDetailModel> lstrmdm = new List<ReceiveMaterialDetailModel>();
            //lstrmdm = irms.GetRecMaterialDetailByHeaderId(rmdm.ReceivedMaterialHeaderId).ReceivedMaterialDetails;
            return Json(rmdvm, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNonStockMaterials()
        {
            ims = new MaterialService();
            List<MaterialModel> lstmm = new List<MaterialModel>();
            lstmm = ims.GetAllNonStocks().Materials;

            return Json(lstmm, JsonRequestBehavior.AllowGet);
        }



        //PROCEDURES OR FUNCTIONS
        private DataTable ModelToDataTable(ReceiveMaterialHeaderModel rmhm)
        {
            DataTable dt = new DataTable();

            PropertyInfo[] Props = typeof(ReceiveMaterialHeaderModel).GetProperties();

            //for columns
            foreach (PropertyInfo info in Props)
            {
                dt.Columns.Add(info.Name);
            }
            //for rows
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {

                values[i] = Props[i].GetValue(rmhm, null);
            }
            dt.Rows.Add(values);
            return dt;
        }

        private DataTable ListRMDMToDataTable(List<ReceiveMaterialDetailModel> lstrmdm)
        {
            DataTable dt = new DataTable();

            PropertyInfo[] Props = typeof(ReceiveMaterialDetailModel).GetProperties();

            //for columns
            foreach (PropertyInfo info in Props)
            {
                dt.Columns.Add(info.Name);
            }

            //for rows
            foreach (ReceiveMaterialDetailModel item in lstrmdm)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dt.Rows.Add(values);
            }

            return dt;
        }

        private DataTable ListRMBDMToDataTable(List<ReceiveMaterialBalanceDetailModel> lstrmbdm)
        {
            DataTable dt = new DataTable();

            PropertyInfo[] Props = typeof(ReceiveMaterialBalanceDetailModel).GetProperties();

            //for columns
            foreach (PropertyInfo info in Props)
            {
                dt.Columns.Add(info.Name);
            }

            //for rows
            if (lstrmbdm != null)
            {
                foreach (ReceiveMaterialBalanceDetailModel item in lstrmbdm)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {

                        values[i] = Props[i].GetValue(item, null);
                    }
                    dt.Rows.Add(values);
                }
            }
            return dt;
        }



    }
}