using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccess;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.Queries.RoleQueries;

namespace TAR1DPWHDATA.DataAccesses.RoleAccess
{
    public class RoleAccess : ConnectionAccess, IRoleAccess
    {
        public RoleViewModel GetAllRoles()
        {
            RoleViewModel rvm = new RoleViewModel();
            List<RoleModel> lstroles = new List<RoleModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = RoleQueries.sqlGetAllRoles;

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            lstroles.Add(new RoleModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Code = dr["code"].ToString(),
                                Role = dr["role"].ToString()
                            });
                        }
                        rvm.IsError = false;
                        rvm.ProcessMessage = "Successfully Retrieved.";
                    }
                    else
                    {
                        lstroles = null;
                        rvm.IsError = false;
                        rvm.ProcessMessage = "No Data.";
                    }
                }
                catch (Exception ex)
                {
                    lstroles = null;
                    rvm.IsError = false;
                    rvm.ProcessMessage = "An error occured: \n" + ex.Message.ToString();
                }
                finally
                {
                    rvm.Roles = lstroles;
                    da.SelectCommand.Connection.Close();
                }
            }

            return rvm;
        }

        public ProcessViewModel InsertRole(RoleModel role)
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
                    cmd.CommandText = RoleQueries.sqlInsertRole;

                    cmd.Parameters.AddWithValue("@code", role.Code);
                    cmd.Parameters.AddWithValue("@description", role.Role);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "New Role Saved.";
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

        public ProcessViewModel RemoveRole(int id)
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
                    cmd.CommandText = RoleQueries.sqlDeleteRole;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "Role Removed.";
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

        public ProcessViewModel UpdateRole(RoleModel role)
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
                    cmd.CommandText = RoleQueries.sqlUpdateRole;

                    cmd.Parameters.AddWithValue("@id", role.Id);
                    cmd.Parameters.AddWithValue("@code", role.Code.ToString());
                    cmd.Parameters.AddWithValue("@role", role.Role);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "Updated Selected Role.";
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