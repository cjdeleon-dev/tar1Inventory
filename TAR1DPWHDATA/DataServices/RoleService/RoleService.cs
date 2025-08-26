using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccesses.RoleAccess;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.RoleService
{
    public class RoleService : IRoleService
    {
        IRoleAccess ira;

        public RoleService()
        {
            this.ira = new RoleAccess();
        }

        public RoleViewModel GetAllRoles()
        {
            return ira.GetAllRoles();
        }

        public ProcessViewModel InsertRole(RoleModel role)
        {
            return ira.InsertRole(role);
        }

        public ProcessViewModel RemoveRole(int id)
        {
            return ira.RemoveRole(id);
        }

        public ProcessViewModel UpdateRole(RoleModel role)
        {
            return ira.UpdateRole(role);
        }
    }
}