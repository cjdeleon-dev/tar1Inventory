using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccess;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.Queries.DepartmentQueries;

namespace TAR1DPWHDATA.DataAccesses.DepartmentAccess
{
    public class DepartmentAccess : ConnectionAccess, IDepartmentAccess
    {
        public DepartmentViewModel GetAllDepartments()
        {
            DepartmentViewModel dvm = new DepartmentViewModel();
            List<DepartmentModel> lstDepts = new List<DepartmentModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = DepartmentQueries.sqlGetAllDepartments;

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            lstDepts.Add(new DepartmentModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Code = dr["code"].ToString(),
                                Description = dr["description"].ToString(),
                            });
                        }
                        dvm.IsError = false;
                        dvm.ProcessMessage = "Successfully Retrieved.";
                    }
                    else
                    {
                        lstDepts = null;
                        dvm.IsError = false;
                        dvm.ProcessMessage = "No Data.";
                    }
                }
                catch (Exception ex)
                {
                    lstDepts = null;
                    dvm.IsError = false;
                    dvm.ProcessMessage = "An error occured: \n" + ex.Message.ToString();
                }
                finally
                {
                    dvm.Departments = lstDepts;
                    da.SelectCommand.Connection.Close();
                }
            }

            return dvm;
        }

        public ProcessViewModel InsertDepartment(DepartmentModel unit)
        {
            ProcessViewModel pvm = new ProcessViewModel();

            using (SqlCommand cmd = new SqlCommand())
            {
                SqlConnection con = new SqlConnection(this.ConnectionString);
                SqlTransaction trans = null;
                try
                {
                    con.Open();
                    trans = con.BeginTransaction();
                    cmd.Connection = con;
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = DepartmentQueries.sqlInsertDepartment;

                    cmd.Parameters.AddWithValue("@code", unit.Code);
                    cmd.Parameters.AddWithValue("@description", unit.Description);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "New Department Saved.";
                }
                catch (Exception ex)
                {
                    trans.Rollback();

                    pvm.IsError = true;
                    pvm.ProcessMessage = "An error occured: \n" + ex.Message;
                }
                finally
                {
                    trans.Dispose();
                    con.Close();
                }

            }

            return pvm;
        }

        public ProcessViewModel RemoveDepartment(int id)
        {
            ProcessViewModel pvm = new ProcessViewModel();

            using (SqlCommand cmd = new SqlCommand())
            {
                SqlConnection con = new SqlConnection(this.ConnectionString);
                SqlTransaction trans = null;
                try
                {
                    con.Open();
                    trans = con.BeginTransaction();
                    cmd.Connection = con;
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = DepartmentQueries.sqlRemoveDepartment;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "Department Removed.";
                }
                catch (Exception ex)
                {
                    trans.Rollback();

                    pvm.IsError = true;
                    pvm.ProcessMessage = "An error occured: \n" + ex.Message;
                }
                finally
                {
                    trans.Dispose();
                    con.Close();
                }

            }

            return pvm;
        }

        public ProcessViewModel UpdateDepartment(DepartmentModel unit)
        {
            ProcessViewModel pvm = new ProcessViewModel();

            using (SqlCommand cmd = new SqlCommand())
            {
                SqlConnection con = new SqlConnection(this.ConnectionString);
                SqlTransaction trans = null;
                try
                {
                    con.Open();
                    trans = con.BeginTransaction();
                    cmd.Connection = con;
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = DepartmentQueries.sqlUpdateDepartment;

                    cmd.Parameters.AddWithValue("@id", unit.Id);
                    cmd.Parameters.AddWithValue("@code", unit.Code.ToString());
                    cmd.Parameters.AddWithValue("@description", unit.Description);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "Updated Selected Department.";
                }
                catch (Exception ex)
                {
                    trans.Rollback();

                    pvm.IsError = true;
                    pvm.ProcessMessage = "An error occured: \n" + ex.Message;
                }
                finally
                {
                    trans.Dispose();
                    con.Close();
                }

            }

            return pvm;
        }
    }
}