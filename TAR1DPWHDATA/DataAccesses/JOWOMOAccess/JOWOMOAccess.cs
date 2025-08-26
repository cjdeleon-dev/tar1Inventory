using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccess;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.Queries.JOWOMOQueries;

namespace TAR1DPWHDATA.DataAccesses.JOWOMOAccess
{
    public class JOWOMOAccess : ConnectionAccess, IJOWOMOAccess
    {
        public JOWOMOViewModel GetAllJOWOMOs()
        {
            JOWOMOViewModel jwmvm = new JOWOMOViewModel();
            List<JOWOMOModel> lstJowomos = new List<JOWOMOModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = JOWOMOQueries.sqlGetAllJOWOMOs;

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            lstJowomos.Add(new JOWOMOModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Code = dr["code"].ToString(),
                                Description = dr["description"].ToString(),
                                DRAccount = dr["dr_account"].ToString()
                            });
                        }
                        jwmvm.IsError = false;
                        jwmvm.ProcessMessage = "Successfully Retrieved.";
                    }
                    else
                    {
                        lstJowomos = null;
                        jwmvm.IsError = false;
                        jwmvm.ProcessMessage = "No Data.";
                    }
                }
                catch (Exception ex)
                {
                    lstJowomos = null;
                    jwmvm.IsError = false;
                    jwmvm.ProcessMessage = "An error occured: \n" + ex.Message.ToString();
                }
                finally
                {
                    jwmvm.JOWOMOList = lstJowomos;
                    da.SelectCommand.Connection.Close();
                }
            }

            return jwmvm;
        }          
    }
}