using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.DataServices.DepartmentService;
using TAR1DPWHDATA.Filters;

namespace TAR1DPWHWEB.Controllers
{
    public class DepartmentsController : Controller
    {

        IDepartmentService idservice;

        [Authorize(Roles = "Administrator")]
        // GET: Departments
        public ActionResult DepartmentList(string search, int? i)
        {
            idservice = new DepartmentService();

            List <DepartmentModel> lstDepts = idservice.GetAllDepartments().Departments;
            if (lstDepts != null)
            {
                if (search == null)
                    return View(lstDepts.ToPagedList(i ?? 1, 10));
                else
                    return View(lstDepts.Where(x => x.Code.ToLower().StartsWith(search.ToLower())).ToPagedList(i ?? 1, 10));
            }
            else
            {
                return View(lstDepts);
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public JsonResult AddDepartment(DepartmentModel dept)
        {
            idservice = new DepartmentService();
            return Json(idservice.InsertDepartment(dept), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator")]
        public JsonResult GetDepartmentById(int id)
        {
            idservice = new DepartmentService();
            DepartmentModel dept = idservice.GetAllDepartments().Departments.Find(x => x.Id.Equals(id));
            return Json(dept, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator")]
        public JsonResult UpdateDepartment(DepartmentModel dept)
        {
            idservice = new DepartmentService();
            return Json(idservice.UpdateDepartment(dept), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator")]
        public JsonResult RemoveDepartment(int id)
        {
            idservice = new DepartmentService();
            return Json(idservice.RemoveDepartment(id), JsonRequestBehavior.AllowGet);
        }
    }
}