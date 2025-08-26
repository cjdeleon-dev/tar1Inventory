using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccess;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.Queries.UserQueries;

namespace TAR1DPWHDATA.DataAccesses.UserAccess
{
    public class UserAccess : ConnectionAccess, IUserAccess
    {
        public ProcessViewModel ChangeUserCredentials(UserModel user)
        {
            throw new NotImplementedException();
        }

        public ProcessViewModel DeactivateUser(int id)
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
                    cmd.CommandText = UserQueries.sqlDeactivateUser;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "User Deactivated.";
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

        public UserViewModel GetAllUsers()
        {
            SecureLib11.Encryptor decryptor = new SecureLib11.Encryptor();

            UserViewModel uvm = new UserViewModel();
            List<UserModel> lstUsers = new List<UserModel>();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = UserQueries.sqlGetAllUsers;

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            lstUsers.Add(new UserModel
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                FirstName = dr["firstname"].ToString(),
                                MiddleInitial = dr["middleinitial"].ToString(),
                                LastName = dr["lastname"].ToString(),
                                PositionId = Convert.ToInt32(dr["positionid"]),
                                Position = dr["position"].ToString(),
                                Address = dr["address"].ToString(),
                                UserName = decryptor.ByDCode10(dr["username"].ToString()),
                                Password = decryptor.ByDCode10(dr["password"].ToString()),
                                isActive = Convert.ToBoolean(dr["isactive"]),
                                RoleId = Convert.ToInt32(dr["roleid"]),
                                UserPic = dr["userpic"] != DBNull.Value ? (byte[])dr["userpic"]: null
                        });
                        }
                        uvm.IsError = false;
                        uvm.ProcessMessage = "Successfully Retrieved.";
                    }
                    else
                    {
                        lstUsers = null;
                        uvm.IsError = false;
                        uvm.ProcessMessage = "No Data.";
                    }
                }
                catch (Exception ex)
                {
                    lstUsers = null;
                    uvm.IsError = false;
                    uvm.ProcessMessage = "An error occured: \n" + ex.Message.ToString();
                }
                finally
                {
                    uvm.Users = lstUsers;
                    da.SelectCommand.Connection.Close();
                }
            }

            return uvm;
        }

        public ProcessViewModel InsertUser(UserModel user)
        {
            SecureLib11.Encryptor encryptor = new SecureLib11.Encryptor();

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
                    cmd.CommandText = UserQueries.sqlInsertUser;

                    cmd.Parameters.AddWithValue("@firstname", user.FirstName);
                    cmd.Parameters.AddWithValue("@middleinitial", user.MiddleInitial);
                    cmd.Parameters.AddWithValue("@lastname", user.LastName);
                    cmd.Parameters.AddWithValue("@address", user.Address);
                    cmd.Parameters.AddWithValue("@positionid", user.PositionId);
                    cmd.Parameters.AddWithValue("@username", encryptor.ByECode10(user.UserName));
                    cmd.Parameters.AddWithValue("@password", encryptor.ByECode10(user.Password));
                    cmd.Parameters.AddWithValue("@roleid", user.RoleId);

                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "New User Saved.";
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

        public ProcessViewModel UpdateUser(UserModel user)
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
                    cmd.CommandText = UserQueries.sqlUpdateUser;

                    cmd.Parameters.AddWithValue("@id", user.Id);
                    cmd.Parameters.AddWithValue("@firstname", user.FirstName);
                    cmd.Parameters.AddWithValue("@middleinitial", user.MiddleInitial);
                    cmd.Parameters.AddWithValue("@lastname", user.LastName);
                    cmd.Parameters.AddWithValue("@address", user.Address);
                    cmd.Parameters.AddWithValue("@positionid", user.PositionId);
                    cmd.Parameters.AddWithValue("@isactive", user.isActive);
                    cmd.Parameters.AddWithValue("@roleid", user.RoleId);
                    cmd.ExecuteNonQuery();

                    trans.Commit();

                    pvm.IsError = false;
                    pvm.ProcessMessage = "Updated Selected User.";
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