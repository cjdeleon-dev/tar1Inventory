using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccess;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.Queries.AuthenticationQueries;

namespace TAR1DPWHDATA.DataAccesses.AuthenticationAccess
{
    public class AuthenticationAccess : ConnectionAccess, IAuthenticationAccess
    {
        public UserLoginViewModel GetUserByUserCredentials(string username, string password)
        {
            SecureLib11.Encryptor encryptor = new SecureLib11.Encryptor();

            UserLoginViewModel ulvm = new UserLoginViewModel();
            UserLoginModel ulm = new UserLoginModel();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.Connection.Open();

                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = AuthenticationQueries.sqlGetUserByUserCredentials;

                da.SelectCommand.Parameters.AddWithValue("@username", encryptor.ByECode10(username));
                da.SelectCommand.Parameters.AddWithValue("@password", encryptor.ByECode10(password));

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        ulm.Id = Convert.ToInt32(dt.Rows[0]["id"]);
                        ulm.FirstName = dt.Rows[0]["firstname"].ToString();
                        ulm.MiddleInitial = dt.Rows[0]["middleinitial"].ToString();
                        ulm.LastName = dt.Rows[0]["lastname"].ToString();
                        ulm.Position = dt.Rows[0]["position"].ToString();
                        ulm.IsActive = Convert.ToBoolean(dt.Rows[0]["isactive"]);

                        ulvm.UserLogged = ulm;
                        ulvm.IsError = false;
                        ulvm.ProcessMessage = "Successful Authorization.";
                    }
                    else
                    {
                        ulvm.UserLogged = null;
                        ulvm.IsError = false;
                        ulvm.ProcessMessage = "User does not exist or username and password is incorrect.";
                    }
                }
                catch (Exception ex)
                {
                    ulvm.UserLogged = null;
                    ulvm.IsError = true;
                    ulvm.ProcessMessage = "An error occured: \n" + ex.Message.ToString();
                }
                finally
                {
                    da.SelectCommand.Connection.Close();
                }
            }

            return ulvm;
        }

        public List<RoleModel> GetRolesByUserName(string uname)
        {
            List<RoleModel> lstRoles = new List<RoleModel>();

            SecureLib11.Encryptor encryptor = new SecureLib11.Encryptor();

            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = new SqlCommand();
            da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
            da.SelectCommand.Connection.Open();

            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.CommandText = AuthenticationQueries.sqlGetRolesByUserName;

            da.SelectCommand.Parameters.AddWithValue("@uname", encryptor.ByECode10(uname));

            DataTable dt = new DataTable();

            try
            {
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        lstRoles.Add(new RoleModel {
                            Id = Convert.ToInt32(dr["roleid"]),
                            Role = dr["role"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                lstRoles = null;
            }
            finally
            {
                da.SelectCommand.Connection.Close();
                da.Dispose();
            }
            
            return lstRoles;
        }
    }
}