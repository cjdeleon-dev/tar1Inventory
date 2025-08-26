using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccess;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.Queries.AccountQueries;

namespace TAR1DPWHDATA.DataAccesses.AccountAccess
{
    public class AccountAccess : ConnectionAccess, IAccountAccess
    {
        public bool DeletePhoto(int id)
        {
            bool result = false;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(this.ConnectionString);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = AccountQueries.sqlDeletePhoto;

                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    cmd.ExecuteNonQuery();
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }

        public string GetPasswordByID(int id)
        {
            string pass = string.Empty;
            SecureLib11.Encryptor dec = new SecureLib11.Encryptor();

            DataTable dt = new DataTable();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.CommandText = AccountQueries.sqlGetPasswordByID;

                da.SelectCommand.Parameters.AddWithValue("@id", id);

                da.Fill(dt);
            }
            if (dt.Rows.Count > 0)
                pass = dec.ByDCode10(dt.Rows[0]["password"].ToString());

            return pass;
        }

        public ProcessViewModel UpdatePasswordByID(int id, string password)
        {
            ProcessViewModel pvm = new ProcessViewModel();

            SecureLib11.Encryptor enc = new SecureLib11.Encryptor();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(this.ConnectionString);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = AccountQueries.sqlUpdatePasswordById;

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@password", enc.ByECode10(password));

                try
                {
                    cmd.ExecuteNonQuery();
                    pvm.IsError = false;
                    pvm.ProcessMessage = "Password Successfully Updated.";
                }
                catch (Exception ex)
                {
                    pvm.IsError = true;
                    pvm.ProcessMessage = "An error occured: \n" + ex.Message;
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return pvm;
        }

        public bool UpdatePhoto(int Id, byte[] ImageByte)
        {
            bool result = false;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(this.ConnectionString);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = AccountQueries.sqlUpdatePhoto;

                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@userpic", ImageByte);
                try
                {
                    cmd.ExecuteNonQuery();
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }
    }
}