using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataAccesses.UnitAccess
{
    public interface IUnitAccess
    {
        UnitViewModel GetAllUnits();
        ProcessViewModel InsertUnit(UnitModel unit);
        ProcessViewModel UpdateUnit(UnitModel unit);
        ProcessViewModel RemoveUnit(int id);
    }
}
