using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.AuthenticationService
{
    public interface IAuthenticationService
    {
        UserLoginViewModel GetUserByUserCredentials(string username, string password);
        List<RoleModel> GetRolesByUserName(string username);
    }
}
