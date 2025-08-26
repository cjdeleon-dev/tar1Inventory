using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccess;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.Queries.ReceiveMaterialQueries;

namespace TAR1DPWHDATA.DataAccesses.ReceiveMaterialAccess
{
    public class ReceiveMaterialAccess : ConnectionAccess, IReceiveMaterialAccess
    {
        public EmployeeViewModel GetAllEmployees()
        {
            EmployeeViewModel evm = new EmployeeViewModel();
            List<EmployeeModel> lstem = new List<EmployeeModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = ReceivedMaterialQueries.sqlGetEmployees;

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            lstem.Add(new EmployeeModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Name = dr["name"].ToString(),
                                PositionId = Convert.ToInt32(dr["positionid"]),
                                Position = dr["position"].ToString(),
                            });
                        }
                        evm.IsError = false;
                        evm.ProcessMessage = "Successfully Retrieved.";
                    }
                    else
                    {
                        lstem = null;
                        evm.IsError = false;
                        evm.ProcessMessage = "No Data.";
                    }
                }
                catch (Exception ex)
                {
                    lstem = null;
                    evm.IsError = false;
                    evm.ProcessMessage = "An error occured: \n" + ex.Message.ToString();
                }
                finally
                {
                    evm.Employees = lstem;
                    da.SelectCommand.Connection.Close();
                }
            }

            return evm;
        }

        public ReceiveMaterialHeaderViewModel GetAllReceivedMaterialHeaders()
        {
            ReceiveMaterialHeaderViewModel rmhv = new ReceiveMaterialHeaderViewModel();
            List<ReceiveMaterialHeaderModel> lstrmh = new List<ReceiveMaterialHeaderModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = ReceivedMaterialQueries.sqlGetAllReceivedMaterialHeaders;

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            lstrmh.Add(new ReceiveMaterialHeaderModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                ReceivedDate = dr["receiveddate"].ToString(),
                                PreparedById = Convert.ToInt32(dr["preparedbyid"]),
                                PreparedBy = dr["preparedby"].ToString(),
                                PosPrepBy = dr["posprepby"].ToString(),
                                ReceivedTotalCost = Convert.ToDouble(dr["receivedtotalcost"]),
                                IsOld = Convert.ToBoolean(dr["IsOld"]),
                                Supplier = dr["Supplier"].ToString(),
                                ReceivedBy = dr["ReceivedBy"].ToString(),
                                PosRecBy = dr["posrecby"].ToString(),
                                CheckedBy = dr["CheckedBy"].ToString(),
                                PosChckBy = dr["poschckby"].ToString(),
                                NotedBy = dr["NotedBy"].ToString(),
                                PosNoteBy = dr["posnoteby"].ToString(),
                                AuditedBy = dr["AuditedBy"].ToString(),
                                PosAudBy = dr["posaudby"].ToString(),
                                Remark = dr["Remark"].ToString(),
                                PO1 = dr["PO1"].ToString(),
                                PO2 = dr["PO2"].ToString(),
                                PO3 = dr["PO3"].ToString(),
                                PO4 = dr["PO4"].ToString(),
                                PO5 = dr["PO5"].ToString(),
                                SI1 = dr["SI1"].ToString(),
                                SI2 = dr["SI2"].ToString(),
                                SI3 = dr["SI3"].ToString(),
                                SI4 = dr["SI4"].ToString(),
                                SI5 = dr["SI5"].ToString(),
                                DR1 = dr["DR1"].ToString(),
                                DR2 = dr["DR2"].ToString(),
                                DR3 = dr["DR3"].ToString(),
                                DR4 = dr["DR4"].ToString(),
                                DR5 = dr["DR5"].ToString(),
                                DeliveryDate = dr["deliverydate"].ToString()
                            });
                        }
                        rmhv.IsError = false;
                        rmhv.ProcessMessage = "Successfully Retrieved.";
                    }
                    else
                    {
                        lstrmh = null;
                        rmhv.IsError = false;
                        rmhv.ProcessMessage = "No Data.";
                    }
                }
                catch (Exception ex)
                {
                    lstrmh = null;
                    rmhv.IsError = false;
                    rmhv.ProcessMessage = "An error occured: \n" + ex.Message.ToString();
                }
                finally
                {
                    rmhv.ReceivedMaterialHeaders = lstrmh;
                    da.SelectCommand.Connection.Close();
                }
            }

            return rmhv;
        }

        public ReceiveMaterialHeaderViewModel GetAllReceivedNonStockMaterialHeader()
        {
            ReceiveMaterialHeaderViewModel rmhv = new ReceiveMaterialHeaderViewModel();
            List<ReceiveMaterialHeaderModel> lstrmh = new List<ReceiveMaterialHeaderModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = ReceivedMaterialQueries.sqlGetAllReceivedNonStockMaterialHeaders;

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            lstrmh.Add(new ReceiveMaterialHeaderModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                ReceivedDate = dr["receiveddate"].ToString(),
                                PreparedById = Convert.ToInt32(dr["preparedbyid"]),
                                PreparedBy = dr["preparedby"].ToString(),
                                ReceivedTotalCost = Convert.ToDouble(dr["receivedtotalcost"]),
                                Supplier = dr["Supplier"].ToString(),
                                ReceivedBy = dr["ReceivedBy"].ToString(),
                                NotedBy = dr["NotedBy"].ToString(),
                                AuditedBy = dr["AuditedBy"].ToString(),
                                POs = dr["POs"].ToString(),
                                SIs = dr["SIs"].ToString(),
                                DRs = dr["DRs"].ToString()
                            });
                        }
                        rmhv.IsError = false;
                        rmhv.ProcessMessage = "Successfully Retrieved.";
                    }
                    else
                    {
                        lstrmh = null;
                        rmhv.IsError = false;
                        rmhv.ProcessMessage = "No Data.";
                    }
                }
                catch (Exception ex)
                {
                    lstrmh = null;
                    rmhv.IsError = false;
                    rmhv.ProcessMessage = "An error occured: \n" + ex.Message.ToString();
                }
                finally
                {
                    rmhv.ReceivedMaterialHeaders = lstrmh;
                    da.SelectCommand.Connection.Close();
                }
            }

            return rmhv;
        }

        public ReceiveMaterialHeaderModel GetCurrentRMIdByUserId(int id)
        {
            ReceiveMaterialHeaderModel rmhm = new ReceiveMaterialHeaderModel();
            
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = ReceivedMaterialQueries.sqlGetCurrentRMIdByUserId;

                da.SelectCommand.Parameters.AddWithValue("@preparedbyid", id);

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rmhm.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                        rmhm.PreparedById = 0;
                        rmhm.ReceivedDate = string.Empty;
                        rmhm.ReceivedTotalCost = 0;
                    }
                    else
                    {
                        rmhm.Id = 0;
                        rmhm.PreparedById = 0;
                        rmhm.ReceivedDate = string.Empty;
                        rmhm.ReceivedTotalCost = 0;
                    }
                }
                catch (Exception ex)
                {
                    rmhm = null;
                }
                finally
                {
                    da.SelectCommand.Connection.Close();
                }
            }

            return rmhm;
        }

        public ReceiveMaterialHeaderModel GetCurrentRMNSIdByUserId()
        {
            ReceiveMaterialHeaderModel rmhm = new ReceiveMaterialHeaderModel();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = ReceivedMaterialQueries.sqlGetCurrentRMNSIdByUserId;

                da.SelectCommand.Parameters.AddWithValue("@preparedbyid", Globals.GlobalVars.IdLogged);

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rmhm.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                        rmhm.PreparedById = 0;
                        rmhm.ReceivedDate = string.Empty;
                        rmhm.ReceivedTotalCost = 0;
                    }
                    else
                    {
                        rmhm.Id = 0;
                        rmhm.PreparedById = 0;
                        rmhm.ReceivedDate = string.Empty;
                        rmhm.ReceivedTotalCost = 0;
                    }
                }
                catch (Exception ex)
                {
                    rmhm = null;
                }
                finally
                {
                    da.SelectCommand.Connection.Close();
                }
            }

            return rmhm;
        }

        public ReceiveMaterialBalanceDetailViewModel GetRecMaterialBalanceDetailByHeaderid(int rmhdrid)
        {
            ReceiveMaterialBalanceDetailViewModel rmbdvm = new ReceiveMaterialBalanceDetailViewModel();
            List<ReceiveMaterialBalanceDetailModel> lstrmbdm = new List<ReceiveMaterialBalanceDetailModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = ReceivedMaterialQueries.sqlGetRecMaterialBalanceDetailByHeaderid;

                da.SelectCommand.Parameters.AddWithValue("@rmhid", rmhdrid);

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            lstrmbdm.Add(new ReceiveMaterialBalanceDetailModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Material = dr["material"].ToString(),
                                Unit = dr["unit"].ToString(),
                                Quantity = Convert.ToInt32(dr["quantity"]),
                                Remark = dr["remark"].ToString()
                            });
                        }
                        rmbdvm.BalanceMaterials = lstrmbdm;
                        rmbdvm.IsError = false;
                        rmbdvm.ProcessMessage = "Successfully Retrieved.";
                    }
                    else
                    {
                        rmbdvm.BalanceMaterials = null;
                        rmbdvm.IsError = false;
                        rmbdvm.ProcessMessage = "No Data.";
                    }
                }
                catch (Exception ex)
                {
                    rmbdvm.BalanceMaterials = null;
                    rmbdvm.IsError = true;
                    rmbdvm.ProcessMessage = "An error occured: " + ex.Message;
                }
                finally
                {
                    da.SelectCommand.Connection.Close();
                }
            }

            return rmbdvm;
        }

        public ReceiveMaterialDetailViewModel GetRecMaterialDetailByHeaderId(int rmhdrid)
        {
            ReceiveMaterialDetailViewModel rmdvm = new ReceiveMaterialDetailViewModel();
            List<ReceiveMaterialDetailModel> lstrmdm = new List<ReceiveMaterialDetailModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = ReceivedMaterialQueries.sqlGetRecMaterialDetailByHeaderId;

                da.SelectCommand.Parameters.AddWithValue("@rmhdrid", rmhdrid);

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach(DataRow dr in dt.Rows)
                        {
                            //dtl.id, recievedmaterialheaderid, materialid,mat.Description[material],dtl.quantity ,dtl.unitid,unt.Description[unit],unitcost,totalcost,dtl.onhand
                            lstrmdm.Add(new ReceiveMaterialDetailModel {
                                Id = Convert.ToInt32(dr["id"]),
                                ReceivedMaterialHeaderId = rmhdrid,
                                MaterialId = Convert.ToInt32(dr["materialid"]),
                                Material = dr["material"].ToString(),
                                UnitId = Convert.ToInt32(dr["unitid"]),
                                Unit = dr["unit"].ToString(),
                                Quantity = Convert.ToInt32(dr["quantity"]),
                                UnitCost = Convert.ToDouble(dr["unitcost"]),
                                TotalCost = Convert.ToDouble(dr["totalcost"]),
                                InventorialCost = Convert.ToDouble(dr["inventorialcost"]),
                                VAT = Convert.ToDouble(dr["vat"]),
                                OnHand = Convert.ToInt32(dr["onhand"])
                            });
                        }
                        rmdvm.ReceivedMaterialDetails = lstrmdm;
                        rmdvm.IsError = false;
                        rmdvm.ProcessMessage = "Successfully Retrieved.";
                    }
                    else
                    {
                        rmdvm.ReceivedMaterialDetails = null;
                        rmdvm.IsError = false;
                        rmdvm.ProcessMessage = "No Data.";
                    }
                }
                catch (Exception ex)
                {
                    rmdvm.ReceivedMaterialDetails = null;
                    rmdvm.IsError = true;
                    rmdvm.ProcessMessage = "An error occured: " + ex.Message;
                }
                finally
                {
                    da.SelectCommand.Connection.Close();
                }
            }

            return rmdvm;
        }

        public ReceiveMaterialDetailViewModel GetRecMaterialNonStockDetailByHeaderId(int rmnshdrid)
        {
            ReceiveMaterialDetailViewModel rmdvm = new ReceiveMaterialDetailViewModel();
            List<ReceiveMaterialDetailModel> lstrmdm = new List<ReceiveMaterialDetailModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = ReceivedMaterialQueries.sqlGetRecNonStockDetailByHeaderId;

                da.SelectCommand.Parameters.AddWithValue("@rmhdrid", rmnshdrid);

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            //dtl.id, recievedmaterialheaderid, materialid,mat.Description[material],dtl.quantity ,dtl.unitid,unt.Description[unit],unitcost,totalcost,dtl.onhand
                            lstrmdm.Add(new ReceiveMaterialDetailModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                ReceivedMaterialHeaderId = rmnshdrid,
                                MaterialId = Convert.ToInt32(dr["nonstockid"]),
                                Material = dr["material"].ToString(),
                                UnitId = Convert.ToInt32(dr["unitid"]),
                                Unit = dr["unit"].ToString(),
                                Quantity = Convert.ToInt32(dr["quantity"]),
                                UnitCost = Convert.ToDouble(dr["unitcost"]),
                                TotalCost = Convert.ToDouble(dr["totalcost"]),
                                InventorialCost = Convert.ToDouble(dr["inventorialcost"]),
                                VAT = Convert.ToDouble(dr["vat"]),
                                OnHand = Convert.ToInt32(dr["onhand"])
                            });
                        }
                        rmdvm.ReceivedMaterialDetails = lstrmdm;
                        rmdvm.IsError = false;
                        rmdvm.ProcessMessage = "Successfully Retrieved.";
                    }
                    else
                    {
                        rmdvm.ReceivedMaterialDetails = null;
                        rmdvm.IsError = false;
                        rmdvm.ProcessMessage = "No Data.";
                    }
                }
                catch (Exception ex)
                {
                    rmdvm.ReceivedMaterialDetails = null;
                    rmdvm.IsError = true;
                    rmdvm.ProcessMessage = "An error occured: " + ex.Message;
                }
                finally
                {
                    da.SelectCommand.Connection.Close();
                }
            }

            return rmdvm;
        }

        public List<RRExportModel> GetRRExportData(string dateFrom, string dateTo)
        {
            List<RRExportModel> lstRRem = new List<RRExportModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();
                da.SelectCommand.CommandTimeout = 90000000;

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.CommandText = "sp_getAllRRbyDateRange";

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

                            lstRRem.Add(new RRExportModel
                            {
                                RRNo = Convert.ToInt32(dr["Id"]),
                                ReceivedDate = dr["ReceivedDate"].ToString(),
                                PreparedBy = dr["PreparedBy"].ToString(),
                                ReceivedTotalCost = Convert.ToDouble(dr["ReceivedTotalCost"]),
                                Supplier = dr["Supplier"].ToString(),
                                PONos = dr["PONos"].ToString(),
                                SINos = dr["SINos"].ToString(),
                                DRNos = dr["DRNos"].ToString(),
                                DeliveryDate = dr["DeliveryDate"].ToString(),
                                Remark = dr["Remark"].ToString(),
                                Material = dr["Material"].ToString(),
                                Description = dr["Description"].ToString(),
                                Quantity = Convert.ToInt32(dr["Quantity"]),
                                Unit = dr["Code"].ToString(),
                                UnitCost = Convert.ToDouble(dr["UnitCost"]),
                                TotalCost = Convert.ToDouble(dr["TotalCost"]),
                                InventorialCost = Convert.ToDouble(dr["VAT"]), 
                                VAT = Convert.ToDouble(dr["VAT"]),
                                OnHand = Convert.ToInt32(dr["OnHand"]),
                                BalanceQty = Convert.ToInt32(dr["BalanceQty"]),
                                BalanceRemark=dr["BalanceRemark"].ToString()
                            });
                        }

                    }
                    else
                    {
                        lstRRem = null;
                    }
                }
                catch (Exception ex)
                {
                    lstRRem = null;
                }
                finally
                {
                    da.SelectCommand.Connection.Close();
                }
            }

            return lstRRem;
        }

        public int GetUnitIdByMaterialId(int materialId)
        {
            int rval = 0;

            SqlConnection conn = new SqlConnection(this.ConnectionString);
            using (SqlCommand cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@materialid", materialId);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select DefaultUnitId from Materials where id=@materialid;";

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            rval = Convert.ToInt32(dr["DefaultUnitId"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    rval = 0;
                }
            }
            conn.Close();

            return rval;
        }

        public ProcessViewModel InsertReceivedMaterialDetail(List<ReceiveMaterialDetailModel> lstrmdm)
        {
            ProcessViewModel pvm = new ProcessViewModel();

            if (lstrmdm != null)
            {
                SqlConnection con = new SqlConnection(this.ConnectionString);
                SqlTransaction trans = null;

                con.Open();
                trans = con.BeginTransaction();

                foreach (ReceiveMaterialDetailModel rmdm in lstrmdm)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.Connection = con;
                            cmd.Transaction = trans;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = ReceivedMaterialQueries.sqlInsertReceivedMaterialDetail;
                            //@RecievedMaterialHeaderId,@MaterialId,@Quantity,@UnitId,@UnitCost,@OnHand


                            string rem = string.Empty;

                            if (rmdm.Remark == null)
                            {
                                rem = "";
                            }
                            else
                            {
                                rem = rmdm.Remark;
                            }

                            //getting Inventorial Cost
                            rmdm.InventorialCost = (rmdm.TotalCost / 1.12);
                            //getting VAT
                            rmdm.VAT = (rmdm.InventorialCost * 0.12);
                            //getting Unit Cost
                            rmdm.UnitCost = (rmdm.InventorialCost / rmdm.Quantity);

                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@RecievedMaterialHeaderId", rmdm.ReceivedMaterialHeaderId);
                            cmd.Parameters.AddWithValue("@MaterialId", rmdm.MaterialId);
                            cmd.Parameters.AddWithValue("@Quantity", rmdm.Quantity);
                            cmd.Parameters.AddWithValue("@UnitId", rmdm.UnitId);
                            cmd.Parameters.AddWithValue("@UnitCost", rmdm.UnitCost);
                            cmd.Parameters.AddWithValue("@TotalCost", rmdm.TotalCost);
                            cmd.Parameters.AddWithValue("@InventorialCost", rmdm.InventorialCost);
                            cmd.Parameters.AddWithValue("@VAT", rmdm.VAT);
                            cmd.Parameters.AddWithValue("@OnHand", rmdm.OnHand);
                            cmd.Parameters.AddWithValue("@BalanceQty", rmdm.BalanceQty);
                            cmd.Parameters.AddWithValue("@Remark", rem);

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
                    pvm.ProcessMessage = "Received Material Detail Saved.";
                    trans.Commit();
                    con.Close();
                    con.Dispose();
                }
            }

            return pvm;
        }

        public ProcessViewModel InsertReceivedMaterialHeader(ReceiveMaterialHeaderModel rmhm)
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
                    cmd.CommandText = ReceivedMaterialQueries.sqlInsertReceivedMaterialHeader;
                    //@receiveddate,@receivedbyid,@receivedtotalcost

                    cmd.Parameters.AddWithValue("@receiveddate", rmhm.ReceivedDate);
                    cmd.Parameters.AddWithValue("@preparedbyid", rmhm.PreparedById);
                    cmd.Parameters.AddWithValue("@receivedtotalcost", rmhm.ReceivedTotalCost);
                    cmd.Parameters.AddWithValue("@receivedbyid", rmhm.ReceivedById);
                    cmd.Parameters.AddWithValue("@checkedbyid", rmhm.CheckedById);
                    cmd.Parameters.AddWithValue("@notedbyid", rmhm.NotedById);
                    cmd.Parameters.AddWithValue("@auditedbyid", rmhm.AuditedById);
                    cmd.Parameters.AddWithValue("@isold", rmhm.IsOld);
                    cmd.Parameters.AddWithValue("@supplierid", rmhm.SupplierId == null ? 0 : rmhm.SupplierId);
                    cmd.Parameters.AddWithValue("@remark", rmhm.Remark == null ? "" : rmhm.Remark);
                    cmd.Parameters.AddWithValue("@po1", rmhm.PO1 == null ? "" : rmhm.PO1);
                    cmd.Parameters.AddWithValue("@po2", rmhm.PO2 == null ? "" : rmhm.PO2);
                    cmd.Parameters.AddWithValue("@po3", rmhm.PO3 == null ? "" : rmhm.PO3);
                    cmd.Parameters.AddWithValue("@po4", rmhm.PO4 == null ? "" : rmhm.PO4);
                    cmd.Parameters.AddWithValue("@po5", rmhm.PO5 == null ? "" : rmhm.PO5);
                    cmd.Parameters.AddWithValue("@si1", rmhm.SI1 == null ? "" : rmhm.SI1);
                    cmd.Parameters.AddWithValue("@si2", rmhm.SI2 == null ? "" : rmhm.SI2);
                    cmd.Parameters.AddWithValue("@si3", rmhm.SI3 == null ? "" : rmhm.SI3);
                    cmd.Parameters.AddWithValue("@si4", rmhm.SI4 == null ? "" : rmhm.SI4);
                    cmd.Parameters.AddWithValue("@si5", rmhm.SI5 == null ? "" : rmhm.SI5);
                    cmd.Parameters.AddWithValue("@dr1", rmhm.DR1 == null ? "" : rmhm.DR1);
                    cmd.Parameters.AddWithValue("@dr2", rmhm.DR2 == null ? "" : rmhm.DR2);
                    cmd.Parameters.AddWithValue("@dr3", rmhm.DR3 == null ? "" : rmhm.DR3);
                    cmd.Parameters.AddWithValue("@dr4", rmhm.DR4 == null ? "" : rmhm.DR4);
                    cmd.Parameters.AddWithValue("@dr5", rmhm.DR5 == null ? "" : rmhm.DR5);
                    cmd.Parameters.AddWithValue("@deliverydate", rmhm.DeliveryDate);

                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "Received Material Header Saved.";
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

        public ProcessViewModel InsertReceivedMaterialNonStockDetail(ReceiveMaterialDetailModel rmnsdm)
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
                    cmd.CommandText = ReceivedMaterialQueries.sqlInsertReceivedNonStockDetail;
                    //@RecievedMaterialHeaderId,@MaterialId,@Quantity,@UnitId,@UnitCost,@OnHand

                    cmd.Parameters.AddWithValue("@ReceivedNonStockHeaderId", rmnsdm.ReceivedMaterialHeaderId);
                    cmd.Parameters.AddWithValue("@NonStockId", rmnsdm.MaterialId);
                    cmd.Parameters.AddWithValue("@Quantity", rmnsdm.Quantity);
                    cmd.Parameters.AddWithValue("@UnitId", rmnsdm.UnitId);
                    cmd.Parameters.AddWithValue("@UnitCost", rmnsdm.UnitCost);
                    cmd.Parameters.AddWithValue("@TotalCost", rmnsdm.TotalCost);
                    cmd.Parameters.AddWithValue("@InventorialCost", rmnsdm.InventorialCost);
                    cmd.Parameters.AddWithValue("@VAT", rmnsdm.VAT);
                    cmd.Parameters.AddWithValue("@OnHand", rmnsdm.OnHand);

                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "Received Non-Stock Material Detail Saved.";
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

        public ProcessViewModel InsertReceivedNonStockMaterialHeader(ReceiveMaterialHeaderModel rmnshm)
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
                    cmd.CommandText = ReceivedMaterialQueries.sqlInsertReceivedNonStockHeader;
                    //@receiveddate,@receivedbyid,@receivedtotalcost

                    cmd.Parameters.AddWithValue("@receiveddate", rmnshm.ReceivedDate);
                    cmd.Parameters.AddWithValue("@preparedbyid", rmnshm.PreparedById);
                    cmd.Parameters.AddWithValue("@receivedtotalcost", rmnshm.ReceivedTotalCost);
                    cmd.Parameters.AddWithValue("@receivedbyid", rmnshm.ReceivedById);
                    cmd.Parameters.AddWithValue("@notedbyid", rmnshm.NotedById);
                    cmd.Parameters.AddWithValue("@auditedbyid", rmnshm.AuditedById);
                    cmd.Parameters.AddWithValue("@supplierid", rmnshm.SupplierId);
                    cmd.Parameters.AddWithValue("@po1", rmnshm.PO1);
                    cmd.Parameters.AddWithValue("@po2", rmnshm.PO2);
                    cmd.Parameters.AddWithValue("@po3", rmnshm.PO3);
                    cmd.Parameters.AddWithValue("@po4", rmnshm.PO4);
                    cmd.Parameters.AddWithValue("@po5", rmnshm.PO5);
                    cmd.Parameters.AddWithValue("@si1", rmnshm.SI1);
                    cmd.Parameters.AddWithValue("@si2", rmnshm.SI2);
                    cmd.Parameters.AddWithValue("@si3", rmnshm.SI3);
                    cmd.Parameters.AddWithValue("@si4", rmnshm.SI4);
                    cmd.Parameters.AddWithValue("@si5", rmnshm.SI5);
                    cmd.Parameters.AddWithValue("@dr1", rmnshm.DR1);
                    cmd.Parameters.AddWithValue("@dr2", rmnshm.DR2);
                    cmd.Parameters.AddWithValue("@dr3", rmnshm.DR3);
                    cmd.Parameters.AddWithValue("@dr4", rmnshm.DR4);
                    cmd.Parameters.AddWithValue("@dr5", rmnshm.DR5);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "Received Non-Stock Header Saved.";
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