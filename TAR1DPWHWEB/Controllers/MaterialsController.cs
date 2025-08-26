using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.DataServices.MaterialDataService;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TAR1DPWHWEB.Controllers
{
    public class MaterialsController : Controller
    {
        IMaterialService imservice;
        private static GridView gv = new GridView();

        [Authorize(Roles = "Administrator, Employee")]
        // GET: Materials
        public ActionResult MaterialList(string search, int? i)
        {
            return View();
            
        }

        [Authorize(Roles = "Administrator, Employee")]
        [HttpGet]
        public ActionResult loadfordata()
        {
            imservice = new MaterialService();
            var data = imservice.GetAllMaterials().Materials;
            if (gv.DataSource != null)
                gv.DataSource = null;
            gv.DataSource = data;
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator, Employee")]
        [HttpPost]
        public ActionResult ExportToExcel()
        {
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            string datenow = DateTime.Now.ToShortDateString();
            Response.AddHeader("content-disposition", "attachment; filename=ListMaterials" + datenow + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("MaterialList");
        }

        [Authorize(Roles = "Administrator, Employee")]
        [HttpPost]
        public JsonResult AddMaterial(MaterialModel material)
        {
            imservice = new MaterialService();
            return Json(imservice.InsertMaterial(material), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult GetMaterialById(int id)
        {
            imservice = new MaterialService();
            MaterialModel material = imservice.GetAllMaterials().Materials.Find(x => x.Id.Equals(id));
            return Json(material, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult UpdateMaterial(MaterialModel material)
        {
            imservice = new MaterialService();
            return Json(imservice.UpdateMaterial(material), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator")]
        public JsonResult RemoveMaterial(int id)
        {
            imservice = new MaterialService();
            return Json(imservice.RemoveMaterial(id),JsonRequestBehavior.AllowGet);
        }



        [Authorize(Roles = "Administrator, Employee")]
        // GET: Materials
        public ActionResult NonStockList(string search, int? i)
        {
            imservice = new MaterialService();
            List<MaterialModel> lstMaterials = imservice.GetAllNonStocks().Materials;
            if (lstMaterials != null)
            {
                if (search == null)
                    return View(lstMaterials.ToPagedList(i ?? 1, 10));
                else
                    return View(lstMaterials.Where(x => x.Material.ToLower().StartsWith(search.ToLower())).ToPagedList(i ?? 1, 10));
            }
            else
            {
                return View(lstMaterials);
            }

        }
        [Authorize(Roles = "Administrator, Employee")]
        [HttpPost]
        public JsonResult AddNonStock(MaterialModel material)
        {
            imservice = new MaterialService();
            return Json(imservice.InsertNonStock(material), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult GetNonStockById(int id)
        {
            imservice = new MaterialService();
            MaterialModel material = imservice.GetAllNonStocks().Materials.Find(x => x.Id.Equals(id));
            return Json(material, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult UpdateNonStock(MaterialModel material)
        {
            imservice = new MaterialService();
            return Json(imservice.UpdateNonStock(material), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator")]
        public JsonResult RemoveNonStock(int id)
        {
            imservice = new MaterialService();
            return Json(imservice.RemoveNonStock(id), JsonRequestBehavior.AllowGet);
        }
    }
}