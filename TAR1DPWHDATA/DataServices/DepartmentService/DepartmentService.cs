using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccesses.DepartmentAccess;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        IDepartmentAccess idaccess;

        public DepartmentService()
        {
            this.idaccess = new DepartmentAccess();
        }
        public DepartmentViewModel GetAllDepartments()
        {
            return idaccess.GetAllDepartments();
        }

        public ProcessViewModel InsertDepartment(DepartmentModel dept)
        {
            return idaccess.InsertDepartment(dept);
        }

        public ProcessViewModel RemoveDepartment(int id)
        {
            return idaccess.RemoveDepartment(id);
        }

        public ProcessViewModel UpdateDepartment(DepartmentModel dept)
        {
            return idaccess.UpdateDepartment(dept);
        }
    }
}