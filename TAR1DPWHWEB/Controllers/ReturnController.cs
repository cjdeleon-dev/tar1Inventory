using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAR1DPWHDATA.DataModels;
using System.Configuration;
using TAR1DPWHDATA.DataServices.UserService;
using TAR1DPWHDATA.Queries.ChargeQueries;
using TAR1DPWHDATA.DataServices.ChargeService;

namespace TAR1DPWHWEB.Controllers
{
    public class ReturnController : Controller
    {
        IUserService ius;

        [Authorize(Roles = "Administrator, Employee")]
        // GET: Return
        public ActionResult ReturnStockMaterials()
        {
            return View();
        }

        public JsonResult GetAllReturnedStockHeaders()
        {
            List<ReturnedStockHeaderModel> lsrshm = new List<ReturnedStockHeaderModel>();

            lsrshm = getAllReturnedStockHeaders();

            var jsonResult = Json(new { data = lsrshm }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult GetAllReturnedStockDetailsById(int headerid)
        {
            List<ReturnedStockDetailModel> lst = new List<ReturnedStockDetailModel>();

            lst = getAllReturnedStockDetailsById(headerid);

            var jsonResult = Json(new { data = lst }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult GetAllMaterialTypes()
        {
            List<MaterialTypeModel> lst = new List<MaterialTypeModel>();

            lst = getAllMaterialTypes();

            var jsonResult = Json(new { data = lst }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult GetAllYearValues()
        {
            List<YearValueModel> lst = new List<YearValueModel>();

            lst = getAllYearValues();

            var jsonResult = Json(new { data = lst }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult GetAllMaterialsByMaterialTypeId(int id)
        {
            List<MaterialModel> lst = new List<MaterialModel>();

            lst = getMaterialsByMaterialTypeId(id);

            var jsonResult = Json(new { data = lst }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult GetEmployeesAsReturnedBy()
        {
            List<EmployeeModel> lst = new List<EmployeeModel>();

            lst = getEmployeesAsReturnedBy();

            var jsonResult = Json(new { data = lst }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult GetRateByMaterialIdAndYearCode(int matid, int yearcode)
        {
            return Json(new { data = getRateByMaterialIdAndYearCode(matid, yearcode) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLoggedUserName()
        {
            ius = new UserService();
            UserModel um = new UserModel();

            int IdLogged = Convert.ToInt32(System.Web.HttpContext.Current.Session["Id"]);

            um = ius.GetAllUsers().Users.Find(x => x.Id.Equals(IdLogged));

            return Json(um, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddReturnedStockHeader(ReturnedStockHeaderModel rshm)
        {
            return Json(new { data = addReturnedStockHeader(rshm)}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLoggedUserMaxRCHId()
        {
            int loggedid = Convert.ToInt32(System.Web.HttpContext.Current.Session["Id"]);
            
            return Json(new { data = getLoggedUserMaxRCHId(loggedid) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddReturnedStockDetails(List<ReturnedStockDetailModel> lst)
        {
            return Json(new { data = addReturnedStockDetail(lst) }, JsonRequestBehavior.AllowGet);
        }

        //FUNCTIONS AND PROCEDURES
        private List<ReturnedStockHeaderModel> getAllReturnedStockHeaders()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconnstr"].ConnectionString;

            List<ReturnedStockHeaderModel> lst = new List<ReturnedStockHeaderModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(constr);
                da.SelectCommand.Connection.Open();
                da.SelectCommand.CommandTimeout = 90000000;

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandText = "sp_getAllReturnedHeaderMaterials";

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string returnedDate = string.Empty;
                            int returnedbyid= 0;

                            if (dr["ReturnedDate"] != null)
                                returnedDate = Convert.ToDateTime(dr["ReturnedDate"]).ToString("MM/dd/yyyy");

                            if (dr["ReturnedById"] == DBNull.Value)
                                returnedbyid = 0;
                            else
                                returnedbyid = Convert.ToInt32(dr["ReturnedById"]);

                            lst.Add(new ReturnedStockHeaderModel
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                ReturnedDate = returnedDate,
                                ReturnedById = returnedbyid,
                                ReturnedBy = dr["ReturnedBy"].ToString(),
                                CreatedById = Convert.ToInt32(dr["CreatedById"]),
                                CreatedBy = dr["CreatedBy"].ToString(),
                                Remarks = dr["Remarks"].ToString(),
                                EntryDate = Convert.ToDateTime(dr["EntryDate"]).ToString("MM/dd/yyyy")
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

        private List<ReturnedStockDetailModel> getAllReturnedStockDetailsById(int hdrid)
        {
            string constr = ConfigurationManager.ConnectionStrings["getconnstr"].ConnectionString;

            List<ReturnedStockDetailModel> lst = new List<ReturnedStockDetailModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(constr);
                da.SelectCommand.Connection.Open();
                da.SelectCommand.CommandTimeout = 90000000;

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandText = "sp_getAllReturnedMaterialDetailsById";

                da.SelectCommand.Parameters.AddWithValue("@headerid", hdrid);

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            int? mctno = null;

                            if (dr["MCTNo"] == DBNull.Value)
                                mctno = null;
                            else
                                mctno = Convert.ToInt32(dr["MCTNo"]);

                            lst.Add(new ReturnedStockDetailModel
                            {
                                MCTNo =mctno,
                                IsSalvage = Convert.ToBoolean(dr["IsSalvage"]),
                                Material = dr["Material"].ToString(),
                                SerialNo = dr["SerialNo"].ToString(),
                                RateAmount = Convert.ToDouble(dr["RateAmount"]),
                                Quantity = Convert.ToInt32(dr["Quantity"]),
                                TotalAmount = Convert.ToDouble(dr["TotalAmount"])
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

        private List<MaterialTypeModel> getAllMaterialTypes()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconnstr"].ConnectionString;

            List<MaterialTypeModel> lst = new List<MaterialTypeModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(constr);
                da.SelectCommand.Connection.Open();
                da.SelectCommand.CommandTimeout = 90000000;

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = "select id, description from materialtypes;";

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            
                            lst.Add(new MaterialTypeModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Description = dr["description"].ToString()
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

        private List<YearValueModel> getAllYearValues()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconnstr"].ConnectionString;

            List<YearValueModel> lst = new List<YearValueModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(constr);
                da.SelectCommand.Connection.Open();
                da.SelectCommand.CommandTimeout = 90000000;

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = "select id, code from yearvalues;";

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {

                            lst.Add(new YearValueModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Code = dr["code"].ToString()
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

        private List<MaterialModel> getMaterialsByMaterialTypeId(int id)
        {
            string constr = ConfigurationManager.ConnectionStrings["getconnstr"].ConnectionString;

            List<MaterialModel> lst = new List<MaterialModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(constr);
                da.SelectCommand.Connection.Open();
                da.SelectCommand.CommandTimeout = 90000000;

                da.SelectCommand.CommandType = CommandType.Text;
                if(id==1000)
                    da.SelectCommand.CommandText = "select id, description from materials where MaterialTypeId IS NULL";
                else
                    da.SelectCommand.CommandText = "select id, description from materials where MaterialTypeId=@materialtypeid";

                da.SelectCommand.Parameters.AddWithValue("@materialtypeid", id);

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {

                            lst.Add(new MaterialModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Material = dr["description"].ToString()
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

        private List<EmployeeModel> getEmployeesAsReturnedBy()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconnstr"].ConnectionString;

            List<EmployeeModel> lst = new List<EmployeeModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(constr);
                da.SelectCommand.Connection.Open();
                da.SelectCommand.CommandTimeout = 90000000;

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = "select id, name from employees order by name";

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {

                            lst.Add(new EmployeeModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Name = dr["name"].ToString()
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

        private double getRateByMaterialIdAndYearCode(int materialid, int yearcodeid)
        {
            string constr = ConfigurationManager.ConnectionStrings["getconnstr"].ConnectionString;
            double rate = 0;

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    string cmdText = string.Empty;

                    switch (yearcodeid)
                    {
                        case 2:
                            cmdText = "select Yr10To19ReturnRate [rrate] from materials where id=@id;";
                            break;
                        case 3:
                            cmdText = "select Yr20To29ReturnRate [rrate] from materials where id=@id;";
                            break;
                        case 4:
                            cmdText = "select Yr30AboveReturnRate [rrate] from materials where id=@id;";
                            break;
                        default:
                            cmdText = string.Empty;
                            break;
                    }

                    if ((cmdText != string.Empty))
                    {
                        cmd.Connection = con;
                        con.Open();
                        cmd.CommandType = CommandType.Text;

                        cmd.CommandText = cmdText;
                        cmd.Parameters.AddWithValue("@id", materialid);

                        SqlDataReader rdr;

                        rdr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                rate = Convert.ToDouble(rdr["rrate"]);
                            }
                        }
                        else
                        {
                            rate = 0;
                        }
                    }
                    else
                    {
                        rate = 0;
                    }

                }catch(Exception ex)
                {
                    rate = 0;
                }
                finally
                {
                    cmd.Dispose();
                }

            }

                return rate;
        }

        private int addReturnedStockHeader(ReturnedStockHeaderModel rm)
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
                    cmd.CommandText = "insert into returnedchargedMaterialheaders " +
                                      "values(@returneddate,@isconsumer,@returnedbyid,@returnedby," +
                                      "@createdbyid,@remarks,getdate()); ";

                    cmd.Parameters.AddWithValue("@returneddate", rm.ReturnedDate);
                    cmd.Parameters.AddWithValue("@isconsumer", rm.IsConsumer);
                    if(rm.ReturnedById == null)
                        cmd.Parameters.AddWithValue("@returnedbyid", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@returnedbyid", rm.ReturnedById);
                    cmd.Parameters.AddWithValue("@returnedby", rm.ReturnedBy);
                    cmd.Parameters.AddWithValue("@createdbyid", IdLogged);
                    cmd.Parameters.AddWithValue("@remarks", rm.Remarks);

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

        private int getLoggedUserMaxRCHId(int loggedid)
        {
            int result = 0;

            string constr = ConfigurationManager.ConnectionStrings["getconnstr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select isnull(max(id),0)[hdrid] from ReturnedChargedMaterialHeaders where createdbyid=@id;";
                    cmd.Parameters.AddWithValue("@id", loggedid);

                    SqlDataReader rdr;

                    rdr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            result = Convert.ToInt32(rdr["hdrid"]);
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    result=0;
                }
                finally
                {
                    cmd.Dispose();
                }

            }

            return result;
        }

        private int addReturnedStockDetail(List<ReturnedStockDetailModel> lst)
        {
            int result = 0;

            string constr = ConfigurationManager.ConnectionStrings["getconnstr"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                SqlConnection con = new SqlConnection(constr);
                SqlTransaction trans = null;

                if (lst.Count() > 0)
                {
                    con.Open();
                    trans = con.BeginTransaction();
                    cmd.Connection = con;
                    cmd.Transaction = trans;

                    try
                    {
                        
                        foreach (ReturnedStockDetailModel rm in lst)
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "insert into returnedchargedMaterialdetails(ReturnedChargedHeaderId,MCTNo,MaterialId,serialno," +
                                              "approxyearid,rateamount,quantity,onhand,totalamount,issalvage) " +
                                              "values(@mcrtno,@mctno,@materialid,@serialno," +
                                              "@yearid,@amount,@quantity,@quantity,@totalamount,@issalvage); ";

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@mcrtno", rm.MCRTNo);
                            if (rm.MCTNo == null)
                                cmd.Parameters.AddWithValue("@mctno", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@mctno", rm.MCTNo);
                            cmd.Parameters.AddWithValue("@materialid", rm.MaterialId);
                            if(rm.SerialNo==null)
                                cmd.Parameters.AddWithValue("@serialno", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@serialno", rm.SerialNo);
                            cmd.Parameters.AddWithValue("@yearid", rm.YearId);
                            cmd.Parameters.AddWithValue("@amount", rm.RateAmount);
                            cmd.Parameters.AddWithValue("@quantity", rm.Quantity);
                            cmd.Parameters.AddWithValue("@totalamount", rm.Quantity * rm.RateAmount);
                            cmd.Parameters.AddWithValue("@issalvage", rm.IsSalvage);
                            cmd.ExecuteNonQuery();
                        }

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
                

            }

            return result;
        }
    }
}