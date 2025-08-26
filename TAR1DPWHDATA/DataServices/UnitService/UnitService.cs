using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccesses.UnitAccess;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.UnitService
{
    public class UnitService : IUnitService
    {
        IUnitAccess iuaccess;

        public UnitService()
        {
            this.iuaccess = new UnitAccess();
        }

        public UnitViewModel GetAllUnits()
        {
            return iuaccess.GetAllUnits();
        }

        public ProcessViewModel InsertUnit(UnitModel unit)
        {
            return iuaccess.InsertUnit(unit);
        }

        public ProcessViewModel RemoveUnit(int id)
        {
            return iuaccess.RemoveUnit(id);
        }

        public ProcessViewModel UpdateUnit(UnitModel unit)
        {
            return iuaccess.UpdateUnit(unit);
        }
    }
}