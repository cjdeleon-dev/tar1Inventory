using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccesses.PositionAccess;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.PositionService
{
    public class PositionService : IPositionService
    {
        IPositionAccess ipaccess;

        public PositionService()
        {
            this.ipaccess = new PositionAccess();
        }
        public PositionViewModel GetAllPositions()
        {
            return ipaccess.GetAllPositions();
        }

        public ProcessViewModel InsertPosition(PositionModel post)
        {
            return ipaccess.InsertPosition(post);
        }

        public ProcessViewModel RemovePosition(int id)
        {
            return ipaccess.RemovePosition(id);
        }

        public ProcessViewModel UpdatePosition(PositionModel post)
        {
            return ipaccess.UpdatePosition(post);
        }
    }
}