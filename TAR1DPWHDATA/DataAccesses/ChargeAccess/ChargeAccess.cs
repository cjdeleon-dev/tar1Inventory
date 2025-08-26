using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccess;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.Queries.ChargeQueries;

namespace TAR1DPWHDATA.DataAccesses.ChargeAccess
{
    public class ChargeAccess : ConnectionAccess, IChargeAccess
    {
        public ChargeMaterialHeaderViewModel GetAllChargedMaterialHeaders()
        {
            ChargeMaterialHeaderViewModel cmhv = new ChargeMaterialHeaderViewModel();
            List<ChargeMaterialHeaderModel> lstcmh = new List<ChargeMaterialHeaderModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = ChargeQueries.sqlGetAllChargedMaterialHeaders;

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            lstcmh.Add(new ChargeMaterialHeaderModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                PostedDate = dr["posteddate"].ToString(),
                                PostedById = Convert.ToInt32(dr["postedbyid"]),
                                PostedBy = dr["postedby"].ToString(),
                                PosPostedBy = dr["pospostedby"].ToString(),
                                IssuedById = Convert.ToInt32(dr["issuedbyid"]),
                                IssuedBy = dr["issuedby"].ToString(),
                                PosIssuedBy = dr["posissuedby"].ToString(),
                                IsConsumerReceived = Convert.ToBoolean(dr["isconsumerreceived"]),
                                ReceivedById = dr["receivedbyid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["receivedbyid"]),
                                ReceivedBy = dr["receivedby"]==DBNull.Value? dr["receivedconsumer"].ToString() : dr["receivedby"].ToString(),
                                ConsumerReceivedBy = dr["receivedconsumer"]==DBNull.Value?null:dr["receivedconsumer"].ToString(),
                                PosReceivedBy = dr["posreceivedby"].ToString()==""?"Consumer": dr["posreceivedby"].ToString(),
                                CheckedById = Convert.ToInt32(dr["checkedbyid"]),
                                CheckedBy = dr["checkedby"].ToString(),
                                PosCheckedBy = dr["poscheckedby"].ToString(),
                                AuditedById = Convert.ToInt32(dr["auditedbyid"]),
                                AuditedBy = dr["auditedby"].ToString(),
                                PosAuditedBy = dr["posauditedby"].ToString(),
                                NotedById = Convert.ToInt32(dr["notedbyid"]),
                                NotedBy = dr["notedby"].ToString(),
                                PosNotedBy = dr["posnotedby"].ToString(),
                                Project = dr["project"].ToString(),
                                ProjectAddress = dr["projectaddress"].ToString(),
                                JOWOMOId = Convert.ToInt32(dr["jowomoid"]),
                                JOWOMOCode = dr["code"].ToString(),
                                JOWOMONumber = dr["jowomonumber"].ToString()
                            });
                        }
                        cmhv.IsError = false;
                        cmhv.ProcessMessage = "Successfully Retrieved.";
                    }
                    else
                    {
                        lstcmh = null;
                        cmhv.IsError = false;
                        cmhv.ProcessMessage = "No Data.";
                    }
                }
                catch (Exception ex)
                {
                    lstcmh = null;
                    cmhv.IsError = false;
                    cmhv.ProcessMessage = "An error occured: \n" + ex.Message.ToString();
                }
                finally
                {
                    cmhv.ChargedMaterialHeaders = lstcmh;
                    da.SelectCommand.Connection.Close();
                }
            }

            return cmhv;
        }

        public ChargeMaterialDetailViewModel GetChargedMaterialDetailsByHeaderId(int cmhdrid)
        {
            ChargeMaterialDetailViewModel cmdvm = new ChargeMaterialDetailViewModel();
            List<ChargeMaterialDetailModel> lstcmdm = new List<ChargeMaterialDetailModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = ChargeQueries.sqlGetChargedMaterialDetailsByHeaderId;

                da.SelectCommand.Parameters.AddWithValue("@cmhdrid", cmhdrid);

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            //dtl.id, recievedmaterialheaderid, materialid,mat.Description[material],dtl.quantity ,dtl.unitid,unt.Description[unit],unitcost,totalcost,dtl.onhand
                            lstcmdm.Add(new ChargeMaterialDetailModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                ChargeMaterialHeaderId = cmhdrid,
                                MaterialId = Convert.ToInt32(dr["materialid"]),
                                Material = dr["material"].ToString(),
                                UnitId = Convert.ToInt32(dr["unitid"]),
                                Unit = dr["unit"].ToString(),
                                Quantity = Convert.ToInt32(dr["quantity"]),
                                SerialNo = dr["serialno"].ToString(),
                                JOWOMOId = Convert.ToInt32(dr["jowomoid"]),
                                JOWOMOCode = dr["code"].ToString(),
                                JOWOMONumber = dr["jowomonumber"].ToString()
                            });
                        }
                        cmdvm.ChargeMaterialDetails = lstcmdm;
                        cmdvm.IsError = false;
                        cmdvm.ProcessMessage = "Successfully Retrieved.";
                    }   
                    else
                    {
                        cmdvm.ChargeMaterialDetails = null;
                        cmdvm.IsError = false;
                        cmdvm.ProcessMessage = "No Data.";
                    }
                }
                catch (Exception ex)
                {
                    cmdvm.ChargeMaterialDetails = null;
                    cmdvm.IsError = true;
                    cmdvm.ProcessMessage = "An error occured: " + ex.Message;
                }
                finally
                {
                    da.SelectCommand.Connection.Close();
                }
            }

            return cmdvm;
        }

        public ChargeMaterialHeaderModel GetCurrentCMHIdByUserId(int id)
        {
            ChargeMaterialHeaderModel cmhm = new ChargeMaterialHeaderModel();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = ChargeQueries.sqlGetCurrentCMHIdByUserId;

                da.SelectCommand.Parameters.AddWithValue("@postedbyid", id);

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        cmhm.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                        //cmhm.ChargedDate = string.Empty;
                        //cmhm.ChargedById = 0;
                        //cmhm.ChargedToId = 0;
                        //cmhm.Project = string.Empty;
                        //cmhm.ProjectAddress = string.Empty;
                    }
                    else
                    {
                        cmhm.Id = 0;
                        //cmhm.ChargedDate = string.Empty;
                        //cmhm.ChargedById = 0;
                        //cmhm.ChargedToId = 0;
                        //cmhm.Project = string.Empty;
                        //cmhm.ProjectAddress = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    cmhm = null;
                }
                finally
                {
                    da.SelectCommand.Connection.Close();
                }
            }

            return cmhm;
        }

        public string GetUnitAndOnHandByMaterialId(int matid)
        {
            string result=string.Empty;

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = ChargeQueries.sqlGetUnitAndOnHandByMaterialId;

                da.SelectCommand.Parameters.AddWithValue("@matid", matid);

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        result = dt.Rows[0]["UnitId"].ToString() + "#" + dt.Rows[0]["unit"].ToString() + "#" + dt.Rows[0]["onhand"].ToString();
                    }
                    else
                    {
                        result = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    result = string.Empty;
                }
                finally
                {
                    da.SelectCommand.Connection.Close();
                }
            }

            return result;
        }

        public ProcessViewModel InsertChargedMaterialDetails(List<ChargeMaterialDetailModel> lstcmdm)
        {
            ProcessViewModel pvm = new ProcessViewModel();

            if(lstcmdm != null)
            {
                SqlConnection con = new SqlConnection(this.ConnectionString);
                SqlTransaction trans = null;

                con.Open();
                trans = con.BeginTransaction();

                foreach(ChargeMaterialDetailModel cmdm in lstcmdm)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.Connection = con;
                            cmd.Transaction = trans;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = ChargeQueries.sqlInsertChargedMaterialDetail;

                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@ChargedMaterialHeaderId", cmdm.ChargeMaterialHeaderId);
                            cmd.Parameters.AddWithValue("@MaterialId", cmdm.MaterialId);
                            cmd.Parameters.AddWithValue("@Quantity", cmdm.Quantity);
                            cmd.Parameters.AddWithValue("@UnitId", cmdm.UnitId);
                            cmd.Parameters.AddWithValue("@SerialNo", cmdm.SerialNo ?? string.Empty);
                            cmd.Parameters.AddWithValue("@jowomoid", cmdm.JOWOMOId);
                            cmd.Parameters.AddWithValue("@jowomonumber", cmdm.JOWOMONumber);

                            cmd.ExecuteNonQuery();

                            pvm.IsError = false;

                        }
                        catch (Exception ex)
                        {
                            pvm.IsError = true;
                            pvm.ProcessMessage = "An error occured: \n" + ex.Message;
                        }
                    }
                }

                if (pvm.IsError)
                {
                    trans.Rollback();
                }
                else
                {
                    trans.Commit();
                    con.Close();
                    con.Dispose();
                    pvm = PerformMCTApply(lstcmdm[0].ChargeMaterialHeaderId);
                }
            }

            return pvm;
        }

        private ProcessViewModel PerformMCTApply(int mcthdrid)
        {
            ProcessViewModel pvm = new ProcessViewModel();

            if (mcthdrid > 0)
            {
                SqlConnection con = new SqlConnection(this.ConnectionString);

                using (SqlCommand cmd = new SqlCommand("sp_executeMCTApplybyRR"))
                {
                    try
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@mcthdrid", mcthdrid);

                        cmd.ExecuteNonQuery();
                        pvm.IsError = false;
                        pvm.ProcessMessage = "Successfully Performed.";
                        
                    }
                    catch (Exception ex)
                    {
                        pvm.IsError = true;
                        pvm.ProcessMessage = "An error occured: \n" + ex.Message;
                    }
                    finally
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }

            return pvm;
        }

        public ProcessViewModel InsertChargedMaterialHeader(ChargeMaterialHeaderModel cmhm)
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
                    cmd.CommandText = ChargeQueries.sqlInsertChargedMaterialHeader;

                    //@posteddate,@postedbyid,@issuedbyid,@receivedbyid,
                    //@checkedbyid,@auditedbyid,@notedbyid,@project,@projectaddress,@jowomoid,@jowomonumber

                    cmd.Parameters.AddWithValue("@posteddate", cmhm.PostedDate);
                    cmd.Parameters.AddWithValue("@postedbyid", cmhm.PostedById);
                    cmd.Parameters.AddWithValue("@issuedbyid", cmhm.IssuedById);
                    cmd.Parameters.AddWithValue("@isconsumerreceived", cmhm.IsConsumerReceived);
                    if (cmhm.IsConsumerReceived)
                    {
                        cmd.Parameters.AddWithValue("@receivedbyid", DBNull.Value);
                        cmd.Parameters.AddWithValue("@receivedconsumer", cmhm.ConsumerReceivedBy);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@receivedbyid", cmhm.ReceivedById);
                        cmd.Parameters.AddWithValue("@receivedconsumer", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@checkedbyid", cmhm.CheckedById);
                    cmd.Parameters.AddWithValue("@auditedbyid", cmhm.AuditedById);
                    cmd.Parameters.AddWithValue("@notedbyid", cmhm.NotedById);
                    cmd.Parameters.AddWithValue("@project", cmhm.Project);
                    cmd.Parameters.AddWithValue("@projectaddress", cmhm.ProjectAddress);
                    cmd.Parameters.AddWithValue("@jowomoid", cmhm.JOWOMOId);
                    cmd.Parameters.AddWithValue("@jowomonumber", cmhm.JOWOMONumber);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "Charged Material Header Saved.";
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

        public List<MCTExportModel> GetMCTExportModels(string dateFrom, string dateTo)
        {
            List<MCTExportModel> lstMCTEm = new List<MCTExportModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();
                da.SelectCommand.CommandTimeout = 90000000;

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandText = "sp_getAllMCTbyDateRange";

                da.SelectCommand.Parameters.AddWithValue("@datefr", dateFrom);
                da.SelectCommand.Parameters.AddWithValue("@dateto", dateTo);

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            
                            lstMCTEm.Add(new MCTExportModel
                            {
                                MaterialId = Convert.ToInt32(dr["MaterialId"]),
                                StockName = dr["StockName"].ToString(),
                                StockDescription = dr["StockDescription"].ToString(),
                                SerialNo = dr["SerialNo"].ToString(),
                                PostedDate = Convert.ToDateTime(dr["PostedDate"]).ToString("MM-dd-yyyy"),
                                MCTNo = Convert.ToInt32(dr["MCTNo"]),
                                Unit = dr["Unit"].ToString(),
                                Quantity = Convert.ToInt32(dr["Quantity"]),
                                UnitCost= Convert.ToDouble(dr["UnitCost"]),
                                TotalCost = Convert.ToDouble(dr["TotalCost"]),
                                WOCode =dr["WOCode"].ToString(), 
                                WOAccount = dr["WOAccount"].ToString(),
                                WONumber = dr["WONumber"].ToString(),
                                Project = dr["Project"].ToString(),
                                ProjectAddress = dr["ProjectAddress"].ToString()
                            });
                        }
                        
                    }
                    else
                    {
                        lstMCTEm = null;
                    }
                }
                catch (Exception ex)
                {
                    lstMCTEm = null;
                }
                finally
                {
                    da.SelectCommand.Connection.Close();
                }
            }

            return lstMCTEm;
        }
    }
}