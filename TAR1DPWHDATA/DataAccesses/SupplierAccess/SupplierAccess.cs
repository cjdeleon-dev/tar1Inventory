using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccess;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.Queries.SupplierQueries;

namespace TAR1DPWHDATA.DataAccesses.SupplierAccess
{
    public class SupplierAccess : ConnectionAccess, ISupplierAccess
    {
        public SupplierViewModel GetAllSuppliers()
        {
            SupplierViewModel svm = new SupplierViewModel();
            List<SupplierModel> lstSuppliers = new List<SupplierModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = SupplierQueries.sqlGetAllSuppliers;

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            lstSuppliers.Add(new SupplierModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Supplier = dr["name"].ToString(),
                                Address = dr["address"].ToString()
                            });
                        }
                        svm.IsError = false;
                        svm.ProcessMessage = "Successfully Retrieved.";
                    }
                    else
                    {
                        lstSuppliers = null;
                        svm.IsError = false;
                        svm.ProcessMessage = "No Data.";
                    }
                }
                catch (Exception ex)
                {
                    lstSuppliers = null;
                    svm.IsError = true;
                    svm.ProcessMessage = "An error occured: \n" + ex.Message.ToString();
                }
                finally
                {
                    svm.Suppliers = lstSuppliers;
                    da.SelectCommand.Connection.Close();
                }
            }

            return svm;
        }

        public ProcessViewModel InsertSupplier(SupplierModel supm)
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
                    cmd.CommandText = SupplierQueries.sqlInsertSupplier;

                    cmd.Parameters.AddWithValue("@supplier", supm.Supplier);
                    cmd.Parameters.AddWithValue("@address", supm.Address);
                    cmd.Parameters.AddWithValue("@createdbyid", supm.CreatedById);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "New Supplier Saved.";
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

        public ProcessViewModel RemoveSupplier(int id)
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
                    cmd.CommandText = SupplierQueries.sqlRemoveSupplier;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "Supplier Removed.";
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

        public ProcessViewModel UpdateSupplier(SupplierModel supm)
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
                    cmd.CommandText = SupplierQueries.sqlUpdateSupplier;

                    cmd.Parameters.AddWithValue("@id", supm.Id);
                    cmd.Parameters.AddWithValue("@supplier", supm.Supplier.ToString());
                    cmd.Parameters.AddWithValue("@address", supm.Address);
                    cmd.Parameters.AddWithValue("@updatedbyid", supm.UpdatedById);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "Updated Selected Supplier.";
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