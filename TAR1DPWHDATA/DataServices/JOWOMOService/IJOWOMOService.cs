using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.JOWOMOService
{
    public interface IJOWOMOService
    {
        JOWOMOViewModel GetAllJOWOMOs();
    }
}
