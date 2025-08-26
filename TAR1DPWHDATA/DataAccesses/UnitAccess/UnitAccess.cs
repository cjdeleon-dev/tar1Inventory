using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccess;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.Queries.UnitQueries;

namespace TAR1DPWHDATA.DataAccesses.UnitAccess
{
    public class UnitAccess : ConnectionAccess, IUnitAccess
    {
        public UnitViewModel GetAllUnits()
        {
            UnitViewModel uvm = new UnitViewModel();
            List<UnitModel> lstUnits = new List<UnitModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = UnitQueries.sqlGetAllUnits;

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            lstUnits.Add(new UnitModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Code = dr["code"].ToString(),
                                Description = dr["description"].ToString()
                            });
                        }
                        uvm.IsError = false;
                        uvm.ProcessMessage = "Successfully Retrieved.";
                    }
                    else
                    {
                        lstUnits = null;
                        uvm.IsError = false;
                        uvm.ProcessMessage = "No Data.";
                    }
                }
                catch (Exception ex)
                {
                    lstUnits = null;
                    uvm.IsError = false;
                    uvm.ProcessMessage = "An error occured: \n" + ex.Message.ToString();
                }
                finally
                {
                    uvm.Units = lstUnits;
                    da.SelectCommand.Connection.Close();
                }
            }

            return uvm;
        }

        public ProcessViewModel InsertUnit(UnitModel unit)
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
                    cmd.CommandText = UnitQueries.sqlInsertUnit;

                    cmd.Parameters.AddWithValue("@code", unit.Code);
                    cmd.Parameters.AddWithValue("@description", unit.Description);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "New Unit Saved.";
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

        public ProcessViewModel RemoveUnit(int id)
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
                    cmd.CommandText = UnitQueries.sqlRemoveUnit;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "Unit Removed.";
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

        public ProcessViewModel UpdateUnit(UnitModel unit)
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
                    cmd.CommandText = UnitQueries.sqlUpdateUnit;

                    cmd.Parameters.AddWithValue("@id", unit.Id);
                    cmd.Parameters.AddWithValue("@code", unit.Code.ToString());
                    cmd.Parameters.AddWithValue("@description", unit.Description);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "Updated Selected Unit.";
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