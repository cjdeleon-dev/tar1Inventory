using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.Queries.JOWOMOQueries
{
    public class JOWOMOQueries
    {
        public static readonly string sqlGetAllJOWOMOs = "select id, code, description,dr_account from jowomo where isnull(isactive,0)=1;";
    }
}