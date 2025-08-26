using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TAR1DPWHDATA.DataModels;
using TAR1DPWHDATA.DataServices.AuthenticationService;
using TAR1DPWHDATA.Globals;

namespace TAR1DPWHWEB.SiteRoleProvider
{
    public class SiteRole : RoleProvider
    {
        IAuthenticationService ias;
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            ias = new AuthenticationService();

            List<RoleModel> lst = new List<RoleModel>();

            lst = ias.GetRolesByUserName(username);

            //if (lst[0].Role.Trim().ToUpper() == "ADMINISTRATOR")
            //    GlobalVars.isAdmin = true;

            string[] result = { lst[0].Role };

            return result;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}