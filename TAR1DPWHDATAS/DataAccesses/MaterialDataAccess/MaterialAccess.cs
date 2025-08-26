using System;
using System.Collections.Generic;
using System.Text;
using TAR1DPWHDATA.DataAccess;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.Queries.MaterialQueries;
using System.Data.SqlClient;
using System.Data;

namespace TAR1DPWHDATA.DataAccesses.MaterialDataAccess
{
    public class MaterialAccess : ConnectionAccess, IMaterialAccess
    {
        public MaterialViewModel GetAllMaterials()
        {
            MaterialViewModel mvm = new MaterialViewModel();
            List<MaterialModel> lstMaterials = new List<MaterialModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = MaterialQueries.sqlGetAllMaterials;

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            lstMaterials.Add(new MaterialModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Material = dr["material"].ToString(),
                                Description = dr["description"].ToString(),
                            });
                        }
                        mvm.IsError = false;
                        mvm.ProcessMessage = "Successfully Retrieved.";
                    }
                    else
                    {
                        lstMaterials = null;
                        mvm.IsError = false;
                        mvm.ProcessMessage = "No Data.";
                    }
                }
                catch (Exception ex)
                {
                    lstMaterials = null;
                    mvm.IsError = false;
                    mvm.ProcessMessage = "An error occured: \n" + ex.Message.ToString();
                }
                finally
                {
                    mvm.Materials = lstMaterials;
                    da.SelectCommand.Connection.Close();
                }
            }

            return mvm;
        }

        public MaterialCUDViewModel InsertMaterial(MaterialModel material)
        {
            MaterialCUDViewModel mvm = new MaterialCUDViewModel();

            using (SqlCommand cmd = new SqlCommand())
            {
                SqlConnection con = new SqlConnection(this.ConnectionString);
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = MaterialQueries.sqlInsertMaterial;

                    cmd.Parameters.AddWithValue("@material", material.Material.ToString());
                    cmd.Parameters.AddWithValue("@description", material.Description);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    mvm.IsError = false;
                    mvm.ProcessMessage = "New Material Saved.";
                }
                catch (Exception ex)
                {
                    trans.Rollback();

                    mvm.IsError = false;
                    mvm.ProcessMessage = "An error occured: \n" + ex.Message;
                }
                finally
                {
                    trans.Dispose();
                    con.Close();
                }
                
            }

            return mvm;
        }

        public MaterialCUDViewModel RemoveMaterial(int id)
        {
            MaterialCUDViewModel mvm = new MaterialCUDViewModel();

            using (SqlCommand cmd = new SqlCommand())
            {
                SqlConnection con = new SqlConnection(this.ConnectionString);
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = MaterialQueries.sqlRemoveMaterial;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    mvm.IsError = false;
                    mvm.ProcessMessage = "Material Removed.";
                }
                catch (Exception ex)
                {
                    trans.Rollback();

                    mvm.IsError = false;
                    mvm.ProcessMessage = "An error occured: \n" + ex.Message;
                }
                finally
                {
                    trans.Dispose();
                    con.Close();
                }

            }

            return mvm;
        }

        public MaterialCUDViewModel UpdateMaterial(MaterialModel material)
        {
            MaterialCUDViewModel mvm = new MaterialCUDViewModel();

            using (SqlCommand cmd = new SqlCommand())
            {
                SqlConnection con = new SqlConnection(this.ConnectionString);
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = MaterialQueries.sqlUpdateMaterial;

                    cmd.Parameters.AddWithValue("@id", material.Id);
                    cmd.Parameters.AddWithValue("@material", material.Material.ToString());
                    cmd.Parameters.AddWithValue("@description", material.Description);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    mvm.IsError = false;
                    mvm.ProcessMessage = "Updated Selected Material.";
                }
                catch (Exception ex)
                {
                    trans.Rollback();

                    mvm.IsError = false;
                    mvm.ProcessMessage = "An error occured: \n" + ex.Message;
                }
                finally
                {
                    trans.Dispose();
                    con.Close();
                }

            }

            return mvm;
        }
    }
}
