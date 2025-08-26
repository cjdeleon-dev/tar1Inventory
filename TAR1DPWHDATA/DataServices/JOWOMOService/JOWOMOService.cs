using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccesses.JOWOMOAccess;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.JOWOMOService
{
    public class JOWOMOService : IJOWOMOService
    {
        IJOWOMOAccess ijowomoa;

        public JOWOMOService()
        {
            this.ijowomoa = new JOWOMOAccess();
        }

        public JOWOMOViewModel GetAllJOWOMOs()
        {
            return ijowomoa.GetAllJOWOMOs();
        }
    }
}