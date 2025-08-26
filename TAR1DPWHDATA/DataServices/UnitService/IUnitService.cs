using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.UnitService
{
    public interface IUnitService
    {
        UnitViewModel GetAllUnits();
        ProcessViewModel InsertUnit(UnitModel unit);
        ProcessViewModel UpdateUnit(UnitModel unit);
        ProcessViewModel RemoveUnit(int id);
    }
}
