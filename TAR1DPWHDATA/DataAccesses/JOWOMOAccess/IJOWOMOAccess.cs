using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataAccesses.JOWOMOAccess
{
    public interface IJOWOMOAccess
    {
        JOWOMOViewModel GetAllJOWOMOs();
    }
}
