using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.DataServices.AccountService;
using TAR1DPWHDATA.DataServices.UserService;
using TAR1DPWHDATA.Filters;
using TAR1DPWHDATA.Globals;
using TAR1DPWHDATA.Queries.AccountQueries;

namespace TAR1DPWHWEB.Controllers
{
    public class AccountController : Controller
    {
        IUserService iuservice;
        IAccountService iaservice;
        // GET: Account
        [Authorize(Roles = "Administrator, Employee")]
        public ActionResult Dashboard()
        {
            iuservice = new UserService();
            //int loggedid = GlobalVars.IdLogged;
            int loggedid = Convert.ToInt32(System.Web.HttpContext.Current.Session["Id"]);
            UserModel um = iuservice.GetAllUsers().Users.Find(x => x.Id.Equals(loggedid));

            if (um != null)
            {
                if (um.UserPic == null)
                {
                    ViewBag.Message = "NO PHOTO";
                    MemoryStream ms = new MemoryStream();
                    Image img = Image.FromFile(Server.MapPath("/Content/images/userdefaultjpg.jpg"));
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    um.UserPic = ms.ToArray();
                }
            }
            return View(um);
        }

        [Authorize(Roles = "Administrator, Employee")]
        [HttpPost]
        public ActionResult UploadPhoto(UserModel um)
        {
            iaservice = new AccountService();

            byte[] imgbyte;
            using (Stream inputStream = um.ImageFile.InputStream)
            {
                MemoryStream ms = inputStream as MemoryStream;
                if (ms == null)
                {
                    ms = new MemoryStream();
                    inputStream.CopyTo(ms);
                }
                imgbyte = ms.ToArray();
            }

            um.UserPic = imgbyte;

            bool success = iaservice.UpdatePhoto(um.Id, um.UserPic);

            return RedirectToAction("Dashboard", "Account");
        }

        [Authorize(Roles = "Administrator, Employee")]
        public JsonResult DeletePhoto(int id)
        {
            iaservice = new AccountService();
            MemoryStream ms = new MemoryStream();
            if (iaservice.DeletePhoto(id))
            {
                Image img = Image.FromFile(Server.MapPath("/Content/images/userdefaultjpg.jpg"));
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            return Json(ms.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator, Employee")]
        public ActionResult ChangePassword()
        {
            iaservice = new AccountService();

            ChangePasswordModel cpm = new ChangePasswordModel();
            //cpm.ID = GlobalVars.IdLogged;
            cpm.ID = Convert.ToInt32(System.Web.HttpContext.Current.Session["Id"]);
            cpm.CurrentPassword = iaservice.GetPasswordByID(cpm.ID);
            cpm.OldPassword = string.Empty;
            cpm.NewPassword = string.Empty;
            cpm.ConfirmPassword = string.Empty;
            return View(cpm);
        }

        [Authorize(Roles = "Administrator, Employee")]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel cpm)
        {
            string currpass = cpm.CurrentPassword;
            string oldpass = cpm.OldPassword;
            string newpass = cpm.NewPassword;
            string confpass = cpm.ConfirmPassword;

            if (ModelState.IsValid)
            {
                if (oldpass != string.Empty &&
                   newpass != string.Empty &&
                   confpass != string.Empty)
                {
                    if (oldpass != currpass)
                    {
                        ModelState.AddModelError("ChangePasswordError", "Old Password did not match to the current password.");
                    }
                    else
                    {
                        if (newpass != confpass)
                        {
                            ModelState.AddModelError("ChangePasswordError", "New and confirm password did not match.");
                        }
                        else //accepted values
                        {
                            iaservice = new AccountService();
                            ProcessViewModel pvm = iaservice.UpdatePasswordByID(cpm.ID, cpm.NewPassword);
                            ViewBag.Message = pvm.ProcessMessage;
                        }
                    }

                }
            }
                return View();

        }

        public JsonResult ReseedNow()
        {
            return Json(isSuccessReseeding(),JsonRequestBehavior.AllowGet);
        }


        private ProcessViewModel isSuccessReseeding()
        {
            ProcessViewModel pvm = new ProcessViewModel();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["getconnstr"].ConnectionString);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_setCurrentRRIdent";

                try
                {
                    cmd.ExecuteNonQuery();
                    pvm.ProcessMessage = "Successful Reseeding";
                    pvm.IsError = false;
                }
                catch(Exception ex  )
                {
                    pvm.ProcessMessage = "Error: " + ex.Message;
                    pvm.IsError = true;
                }

                if (pvm.IsError == false)
                {
                    cmd.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["getconnstr"].ConnectionString);
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_setCurrentMCTIdent";
                    try
                    {
                        cmd.ExecuteNonQuery();
                        pvm.ProcessMessage = "Successful Reseeding";
                        pvm.IsError = false;
                    }
                    catch (Exception ex)
                    {
                        pvm.ProcessMessage = "Error: " + ex.Message;
                        pvm.IsError = true;
                    }
                }
                else
                {
                    return pvm;
                }

                if (pvm.IsError == false)
                {
                    cmd.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["getconnstr"].ConnectionString);
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_setCurrentMCRTIdent";
                    try
                    {
                        cmd.ExecuteNonQuery();
                        pvm.ProcessMessage = "Successful Reseeding";
                        pvm.IsError = false;
                    }
                    catch (Exception ex)
                    {
                        pvm.ProcessMessage = "Error: " + ex.Message;
                        pvm.IsError = true;
                    }
                }
                else
                {
                    return pvm;
                }


            }

            return pvm;
        }
    }
}