using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace TAR1DPWHDATA.DataAccess
{
    public class ConnectionAccess
    {
        protected string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["getconnstr"].ToString();
            }
        }
    }
}
