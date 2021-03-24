using System.Collections.Generic;
using System.Text;

namespace ChargingMonitor
{
    public interface ILog
    {
        public void LogDoorLocked(int ID);
        public void LogDoorUnLocked(int ID);

    }
}
