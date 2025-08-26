using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataAccesses.DepartmentAccess
{
    public interface IDepartmentAccess
    {
        DepartmentViewModel GetAllDepartments();
        ProcessViewModel InsertDepartment(DepartmentModel dept);
        ProcessViewModel UpdateDepartment(DepartmentModel dept);
        ProcessViewModel RemoveDepartment(int id);
    }
}
