using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccess;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.Queries.PositionQueries;

namespace TAR1DPWHDATA.DataAccesses.PositionAccess
{
    public class PositionAccess : ConnectionAccess, IPositionAccess
    {
        public PositionViewModel GetAllPositions()
        {
            PositionViewModel pvm = new PositionViewModel();
            List<PositionModel> lstposts = new List<PositionModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = PositionQueries.sqlGetAllPositions;

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            lstposts.Add(new PositionModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Code = dr["code"].ToString(),
                                Description = dr["description"].ToString(),
                                Department = dr["department"].ToString(),
                                DepartmentId = Convert.ToInt32(dr["DepartmentId"])
                            });
                        }
                        pvm.IsError = false;
                        pvm.ProcessMessage = "Successfully Retrieved.";
                    }
                    else
                    {
                        lstposts = null;
                        pvm.IsError = false;
                        pvm.ProcessMessage = "No Data.";
                    }
                }
                catch (Exception ex)
                {
                    lstposts = null;
                    pvm.IsError = false;
                    pvm.ProcessMessage = "An error occured: \n" + ex.Message.ToString();
                }
                finally
                {
                    pvm.Positions = lstposts;
                    da.SelectCommand.Connection.Close();
                }
            }

            return pvm;
        }

        public ProcessViewModel InsertPosition(PositionModel post)
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
                    cmd.CommandText = PositionQueries.sqlInsertPosition;

                    cmd.Parameters.AddWithValue("@code", post.Code);
                    cmd.Parameters.AddWithValue("@description", post.Description);
                    cmd.Parameters.AddWithValue("@departmentid", post.DepartmentId);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "New Position Saved.";
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

        public ProcessViewModel RemovePosition(int id)
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
                    cmd.CommandText = PositionQueries.sqlRemovePosition;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "Position Removed.";
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

        public ProcessViewModel UpdatePosition(PositionModel post)
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
                    cmd.CommandText = PositionQueries.sqlUpdatePosition;

                    cmd.Parameters.AddWithValue("@id", post.Id);
                    cmd.Parameters.AddWithValue("@code", post.Code.ToString());
                    cmd.Parameters.AddWithValue("@description", post.Description);
                    cmd.Parameters.AddWithValue("@departmentid", post.DepartmentId);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "Updated Selected Position.";
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