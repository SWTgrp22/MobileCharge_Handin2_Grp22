using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingMonitor.Door
{
    public class DoorEventArg : EventArgs
    {
        public bool doorIsopen { get; set; }
    }
}
