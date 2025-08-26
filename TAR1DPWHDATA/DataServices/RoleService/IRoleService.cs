using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.RoleService
{
    public interface IRoleService
    {
        RoleViewModel GetAllRoles();
        ProcessViewModel InsertRole(RoleModel role);
        ProcessViewModel UpdateRole(RoleModel role);
        ProcessViewModel RemoveRole(int id);
    }
}