using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataAccesses.UserAccess
{
    public interface IUserAccess
    {
        UserViewModel GetAllUsers();
        ProcessViewModel InsertUser(UserModel user);
        ProcessViewModel UpdateUser(UserModel user);
        ProcessViewModel DeactivateUser(int id);
        ProcessViewModel ChangeUserCredentials(UserModel user);
    }
}
