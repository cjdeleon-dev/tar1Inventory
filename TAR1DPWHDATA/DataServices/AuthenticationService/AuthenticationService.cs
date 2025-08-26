using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccesses.AuthenticationAccess;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        IAuthenticationAccess iausaccess;

        public AuthenticationService()
        {
            this.iausaccess = new AuthenticationAccess();
        }

        public List<RoleModel> GetRolesByUserName(string username)
        {
            return iausaccess.GetRolesByUserName(username);
        }

        public UserLoginViewModel GetUserByUserCredentials(string username, string password)
        {
            return iausaccess.GetUserByUserCredentials(username, password);
        }
    }
}