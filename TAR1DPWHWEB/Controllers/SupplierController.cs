using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHWEB.Controllers
{
    public class SupplierController : Controller
    {
        [Authorize(Roles = "Administrator, Employee")]
        // GET: Supplier
        public ActionResult Suppliers()
        {
            return View();
        }

        public JsonResult GetAllSuppliers()
        {
            List<SupplierModel> lst = new List<SupplierModel>();

            lst = getAllSuppliers();

            var jsonResult = Json(new { data = lst }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult AddNewSupplier(SupplierModel sm) {

            return Json(new { data = addNewSupplier(sm) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateSupplier(SupplierModel sm)
        {

            return Json(new { data = updateSupplier(sm) }, JsonRequestBehavior.AllowGet);
        }



        //PROCEDURES AND FUNCTIONS
        private List<SupplierModel> getAllSuppliers()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconnstr"].ConnectionString;

            List<SupplierModel> lst = new List<SupplierModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(constr);
                da.SelectCommand.Connection.Open();
                da.SelectCommand.CommandTimeout = 90000000;

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = "select id,name,address,createdbyid,createddate,updatedbyid,lastupdated " +
                                               "from suppliers";

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            
                            lst.Add(new SupplierModel
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Supplier = dr["name"].ToString(),
                                Address = dr["address"].ToString(),
                                CreatedById = 0,
                                CreatedBy = "",
                                UpdatedById = 0,
                                UpdatedBy = ""
                            });
                        }

                    }
                    else
                    {
                        lst = null;
                    }
                }
                catch (Exception ex)
                {
                    lst = null;
                }
                finally
                {
                    da.SelectCommand.Connection.Close();
                }
            }

            return lst;
        } 

        private int addNewSupplier(SupplierModel sm)
        {
            int result = 0;

            string constr = ConfigurationManager.ConnectionStrings["getconnstr"].ConnectionString;

            int IdLogged = Convert.ToInt32(System.Web.HttpContext.Current.Session["Id"]);

            using (SqlCommand cmd = new SqlCommand())
            {
                SqlConnection con = new SqlConnection(constr);
                SqlTransaction trans = null;
                try
                {
                    con.Open();
                    trans = con.BeginTransaction();
                    cmd.Connection = con;
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into suppliers " +
                                      "values(@supplier,@address,@createdbyid,getdate()," +
                                      "NULL,NULL); ";

                    cmd.Parameters.AddWithValue("@supplier", sm.Supplier);
                    cmd.Parameters.AddWithValue("@address", sm.Address);
                    cmd.Parameters.AddWithValue("@createdbyid", IdLogged);

                    cmd.ExecuteNonQuery();

                    trans.Commit();
                    result = 1;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
                finally
                {
                    trans.Dispose();
                    con.Close();
                }


            }

            return result;
        }

        private int updateSupplier(SupplierModel sm)
        {
            int result = 0;

            string constr = ConfigurationManager.ConnectionStrings["getconnstr"].ConnectionString;

            int IdLogged = Convert.ToInt32(System.Web.HttpContext.Current.Session["Id"]);

            using (SqlCommand cmd = new SqlCommand())
            {
                SqlConnection con = new SqlConnection(constr);
                SqlTransaction trans = null;
                try
                {
                    con.Open();
                    trans = con.BeginTransaction();
                    cmd.Connection = con;
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update suppliers set name=@supplier,address=@address," +
                                      "updatedbyid=@updatedbyid,lastupdated=getdate() " +
                                      "where id=@id";

                    cmd.Parameters.AddWithValue("@id", sm.Id);
                    cmd.Parameters.AddWithValue("@supplier", sm.Supplier);
                    cmd.Parameters.AddWithValue("@address", sm.Address);
                    cmd.Parameters.AddWithValue("@updatedbyid", IdLogged);

                    cmd.ExecuteNonQuery();

                    trans.Commit();
                    result = 1;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
                finally
                {
                    trans.Dispose();
                    con.Close();
                }


            }

            return result;
        }
    }
}