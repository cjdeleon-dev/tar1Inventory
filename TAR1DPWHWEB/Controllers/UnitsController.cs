using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.DataServices.UnitService;

namespace TAR1DPWHWEB.Controllers
{
    public class UnitsController : Controller
    {
        IUnitService iuservice;
        // GET: Units
        [Authorize(Roles = "Administrator, Employee")]
        public ActionResult UnitList(string search, int? i)
        {
            iuservice = new UnitService();

            List<UnitModel> lstUnits = iuservice.GetAllUnits().Units;
            if (lstUnits != null)
            {
                if (search == null)
                    return View(lstUnits.ToPagedList(i ?? 1, 10));
                else
                    return View(lstUnits.Where(x => x.Code.ToLower().StartsWith(search.ToLower())).ToPagedList(i ?? 1, 10));
            }
            else
            {
                return View(lstUnits);
            }
        }
        [Authorize(Roles = "Administrator, Employee")]
        [HttpPost]
        public JsonResult AddUnit(UnitModel unit)
        {
            iuservice = new UnitService();
            return Json(iuservice.InsertUnit(unit), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult GetUnitById(int id)
        {
            iuservice = new UnitService();
            UnitModel unit = iuservice.GetAllUnits().Units.Find(x => x.Id.Equals(id));
            return Json(unit, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult UpdateUnit(UnitModel unit)
        {
            iuservice = new UnitService();
            return Json(iuservice.UpdateUnit(unit), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult RemoveUnit(int id)
        {
            iuservice = new UnitService();
            return Json(iuservice.RemoveUnit(id), JsonRequestBehavior.AllowGet);
        }
    }
}