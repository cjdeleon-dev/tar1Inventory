using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataAccesses.AuthenticationAccess
{
    public interface IAuthenticationAccess
    {
        UserLoginViewModel GetUserByUserCredentials(string username, string password);
        List<RoleModel> GetRolesByUserName(string username);
    }
}
