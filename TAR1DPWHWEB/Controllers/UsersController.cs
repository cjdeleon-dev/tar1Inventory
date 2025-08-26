using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.DataServices.PositionService;
using TAR1DPWHDATA.DataServices.RoleService;
using TAR1DPWHDATA.DataServices.UserService;

namespace TAR1DPWHWEB.Controllers
{
    public class UsersController : Controller
    {
        IUserService iuservice;
        IPositionService ipservice;
        IRoleService irservice;
        // GET: Users
        [Authorize(Roles = "Administrator")]
        public ActionResult UserList(string search, int? i)
        {
            iuservice = new UserService();

            List<UserModel> lstUsers = iuservice.GetAllUsers().Users;
            if (lstUsers != null)
            {
                if (search == null)
                    return View(lstUsers.ToPagedList(i ?? 1, 10));
                else
                    return View(lstUsers.Where(x => x.FirstName.ToLower().StartsWith(search.ToLower())).ToPagedList(i ?? 1, 10));
            }
            else
            {
                return View(lstUsers);
            }
        }
        [Authorize(Roles = "Administrator")]
        public JsonResult GetAllPositions()
        {
            ipservice = new PositionService();
            List<PositionModel> lstPosts = ipservice.GetAllPositions().Positions;
            return Json(lstPosts, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator")]
        public JsonResult GetAllRoles()
        {
            irservice = new RoleService();
            List<RoleModel> lstRoles = irservice.GetAllRoles().Roles;
            return Json(lstRoles, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public JsonResult AddUser(UserModel user)
        {
            iuservice = new UserService();
            return Json(iuservice.InsertUser(user), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator")]
        public JsonResult DeactivateUserById(int id)
        {
            iuservice = new UserService();
            return Json(iuservice.DeactivateUser(id), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator")]
        public JsonResult UpdateUser(UserModel user)
        {
            iuservice = new UserService();
            return Json(iuservice.UpdateUser(user), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrator")]
        public JsonResult GetUserByID(int id)
        {
            iuservice = new UserService();
            UserModel user = iuservice.GetAllUsers().Users.Find(x => x.Id.Equals(id));
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}