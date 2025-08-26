using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataAccesses.AccountAccess
{
    public interface IAccountAccess
    {
        bool DeletePhoto(int id);
        bool UpdatePhoto(int Id, byte[] ImageByte);
        string GetPasswordByID(int id);

        ProcessViewModel UpdatePasswordByID(int id, string password);
    }
}
