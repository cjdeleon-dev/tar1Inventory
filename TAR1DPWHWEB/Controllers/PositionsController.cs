using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.DataServices.DepartmentService;
using TAR1DPWHDATA.DataServices.PositionService;

namespace TAR1DPWHWEB.Controllers
{
    public class PositionsController : Controller
    {
        IPositionService ipservice;
        IDepartmentService idservice;

        // GET: Position
        [Authorize(Roles = "Administrator")]
        public ActionResult PositionList(string search, int? i)
        {
            ipservice = new PositionService();

            List<PositionModel> lstPosts = ipservice.GetAllPositions().Positions;
            if (lstPosts != null)
            {
                if (search == null)
                    return View(lstPosts.ToPagedList(i ?? 1, 10));
                else
                    return View(lstPosts.Where(x => x.Code.ToLower().StartsWith(search.ToLower())).ToPagedList(i ?? 1, 10));
            }
            else
            {
                return View(lstPosts);
            }
        }
        [Authorize(Roles = "Administrator")]
        public JsonResult GetAllDepartments()
        {
            idservice = new DepartmentService();
            List<DepartmentModel> lstDepts = idservice.GetAllDepartments().Departments;
            return Json(lstDepts, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator")]
        public JsonResult GetPositionByID(int id)
        {
            ipservice = new PositionService();
            PositionModel post = ipservice.GetAllPositions().Positions.Find(x => x.Id.Equals(id));
            return Json(post, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public JsonResult AddPosition(PositionModel post)
        {
            ipservice = new PositionService();
            return Json(ipservice.InsertPosition(post), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator")]
        public JsonResult UpdatePosition(PositionModel post)
        {
            ipservice = new PositionService();
            return Json(ipservice.UpdatePosition(post), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator")]
        public JsonResult RemovePosition(int id)
        {
            ipservice = new PositionService();
            return Json(ipservice.RemovePosition(id), JsonRequestBehavior.AllowGet);
        }
    }
}