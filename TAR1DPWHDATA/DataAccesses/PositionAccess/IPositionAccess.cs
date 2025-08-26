using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataAccesses.PositionAccess
{
    public interface IPositionAccess
    {
        PositionViewModel GetAllPositions();
        ProcessViewModel InsertPosition(PositionModel post);
        ProcessViewModel UpdatePosition(PositionModel post);
        ProcessViewModel RemovePosition(int id);
    }
}
