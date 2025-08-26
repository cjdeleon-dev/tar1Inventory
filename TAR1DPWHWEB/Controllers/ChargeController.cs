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
using System.Web.UI;
using System.Web.UI.WebControls;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.DataServices.ChargeService;
using TAR1DPWHDATA.DataServices.JOWOMOService;
using TAR1DPWHDATA.DataServices.MaterialDataService;
using TAR1DPWHDATA.DataServices.ReceiveMaterialService;
using TAR1DPWHDATA.DataServices.UnitService;
using TAR1DPWHDATA.DataServices.UserService;
using TAR1DPWHDATA.Globals;

namespace TAR1DPWHWEB.Controllers
{
    public class ChargeController : Controller
    {
        IChargeService ics;
        IUserService ius;
        IMaterialService ims;
        IUnitService iuns;
        IJOWOMOService ijwms;
        IReceiveMaterialService irms;

        // GET: Charge
        [Authorize(Roles = "Administrator, Employee")]
        //public ActionResult ChargedMaterials(string search, int? i)
        public ActionResult ChargedMaterials()
        {

            if (!GlobalVars.isAdmin)
                ViewBag.Message = "Regular";
            else
                ViewBag.Message = "Admin";

            //List<ChargeMaterialHeaderModel> lstcmh = new List<ChargeMaterialHeaderModel>();

            //lstcmh = ics.GetAllChargedMaterialHeaders().ChargedMaterialHeaders;

            //if (lstcmh != null)
            //{
            //    if (search == null)
            //        return View(lstcmh.ToPagedList(i ?? 1, 10));
            //    else
            //        return View(lstcmh.Where(x => x.ReceivedBy.Trim().ToLower().StartsWith(search.ToLower())).ToPagedList(i ?? 1, 10));
            //}
            //else
            //{
            //    return View(lstcmh);
            //}
            return View();
        }

        public JsonResult GetAllMCTHeaders()
        {
            ics = new ChargeService();

            List<ChargeMaterialHeaderModel> lstcmh = new List<ChargeMaterialHeaderModel>();

            lstcmh = ics.GetAllChargedMaterialHeaders().ChargedMaterialHeaders;

            var jsonResult = Json(new { data = lstcmh }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult GetLoggedUserName()
        {
            ius = new UserService();
            UserModel um = new UserModel();

            int IdLogged = Convert.ToInt32(System.Web.HttpContext.Current.Session["Id"]);

            um = ius.GetAllUsers().Users.Find(x => x.Id.Equals(IdLogged));

            return Json(um, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult GetMaterials()
        {
            ims = new MaterialService();
            List<MaterialModel> lstmm = new List<MaterialModel>();
            lstmm = ims.GetAllMaterialsForMCT().Materials;

            return Json(lstmm, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult GetUnits()
        {
            iuns = new UnitService();
            List<UnitModel> lstunit = new List<UnitModel>();
            lstunit = iuns.GetAllUnits().Units;

            return Json(lstunit, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult GetJOWOMOs()
        {
            ijwms = new JOWOMOService();
            List<JOWOMOModel> lstjwms = new List<JOWOMOModel>();
            lstjwms = ijwms.GetAllJOWOMOs().JOWOMOList;

            return Json(lstjwms, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult AddChargedHeader(ChargeMaterialHeaderModel cmhm)
        {
            ics = new ChargeService();
            return Json(ics.InsertChargedMaterialHeader(cmhm), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult GetLoggedUserMaxCMId()
        {
            ics = new ChargeService();
            ChargeMaterialHeaderModel cmhm = new ChargeMaterialHeaderModel();
            int loggedid = Convert.ToInt32(System.Web.HttpContext.Current.Session["Id"]);
            cmhm = ics.GetCurrentCMHIdByUserId(loggedid);
            return Json(cmhm, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult AddChargedMaterialDetail(List<ChargeMaterialDetailModel> lstcmdm)
        {
            ics = new ChargeService();
            return Json(ics.InsertChargedMaterialDetails(lstcmdm), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult GetChargedMaterialDetailByHeaderId(int rmdm)
        {
            ics = new ChargeService();
            List<ChargeMaterialDetailModel> lstcmdm = new List<ChargeMaterialDetailModel>();
            lstcmdm = ics.GetChargedMaterialDetailsByHeaderId(rmdm).ChargeMaterialDetails;
            return Json(new { data = lstcmdm }, JsonRequestBehavior.AllowGet); ;
        }

        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult GetEmployees(ReceiveMaterialDetailModel rmdm)
        {
            irms = new ReceiveMaterialService();
            EmployeeViewModel evm = new EmployeeViewModel();
            evm = irms.GetAllEmployees();
            //List<ReceiveMaterialDetailModel> lstrmdm = new List<ReceiveMaterialDetailModel>();
            //lstrmdm = irms.GetRecMaterialDetailByHeaderId(rmdm.ReceivedMaterialHeaderId).ReceivedMaterialDetails;
            return Json(evm.Employees, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult GetUnitAndOnHandByMaterialId(int matid)
        {
            ics = new ChargeService();
            return Json(ics.GetUnitAndOnHandByMaterialId(matid), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator, Employee")]
        public ActionResult MCTReportView(int rptid)
        {
            LocalReport lr = new LocalReport();
            string p = Path.Combine(Server.MapPath("/Reports"), "rptChargedMaterial.rdlc");
            lr.ReportPath = p;

            DataSet ds = new DataSet();

            //header
            ics = new ChargeService();
            //Int32 maxid = ics.GetCurrentCMHIdByUserId().Id;
            ChargeMaterialHeaderModel chm = new ChargeMaterialHeaderModel();

            chm = ics.GetAllChargedMaterialHeaders().ChargedMaterialHeaders.Find(x => x.Id.Equals(rptid));
            DataTable dtHeader = new DataTable();

            dtHeader = ModelToDataTable(chm);
            ds.Tables.Add(dtHeader);

            //details
            ics = new ChargeService();
            List<ChargeMaterialDetailModel> lstcmdm = new List<ChargeMaterialDetailModel>();
            lstcmdm = ics.GetChargedMaterialDetailsByHeaderId(rptid).ChargeMaterialDetails;
            DataTable dtDetails = new DataTable();

            dtDetails = ListRMDMToDataTable(lstcmdm);
            ds.Tables.Add(dtDetails);

            //ReportDataSource for Header
            ReportDataSource cdhdr = new ReportDataSource("dsMCTHeader", ds.Tables["Table1"]);
            //ReportDataSource for Details
            ReportDataSource cddtl = new ReportDataSource("dsMCTDetails", ds.Tables["Table2"]);

            lr.DataSources.Add(cdhdr);//Header
            lr.DataSources.Add(cddtl);//Details

            string mt, enc, f;
            string[] s;
            Warning[] w;

            //Rendering
            byte[] b = lr.Render("PDF", null, out mt, out enc, out f, out s, out w);

            return File(b, mt);
        }

        private DataTable ModelToDataTable(ChargeMaterialHeaderModel rmhm)
        {
            DataTable dt = new DataTable();

            PropertyInfo[] Props = typeof(ChargeMaterialHeaderModel).GetProperties();

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

        private DataTable ListRMDMToDataTable(List<ChargeMaterialDetailModel> lstcmdm)
        {
            DataTable dt = new DataTable();

            PropertyInfo[] Props = typeof(ChargeMaterialDetailModel).GetProperties();

            //for columns
            foreach (PropertyInfo info in Props)
            {
                dt.Columns.Add(info.Name);
            }

            //for rows
            foreach (ChargeMaterialDetailModel item in lstcmdm)
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

        [Authorize(Roles = "Administrator, Employee")]
        [HttpPost]
        public ActionResult ExportToExcel(FormCollection formCollection)
        {
            ics = new ChargeService();
            string dateFrom = formCollection["dtpFrom"].ToString();
            string dateTo = formCollection["dtpTo"].ToString();
            GridView gv = new GridView();
            List<MCTExportModel> data = ics.GetMCTExportModels(dateFrom, dateTo);
            gv.DataSource = data;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=ChargedMaterials_" + dateFrom + "_" + dateTo + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("ChargedMaterials");
        }

        public JsonResult FetchDateByDateRange(string datefr, string dateto)
        {
            ics = new ChargeService();
            List<MCTExportModel> lst = ics.GetMCTExportModels(datefr, dateto);

            var jsonResult = Json(new { data = lst }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
    }
}