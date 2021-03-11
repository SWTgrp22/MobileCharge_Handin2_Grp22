using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace ChargingMonitor.Door
{
    public interface IDoor
    {
        public event EventHandler<DoorEventArg> doorChangedEvent;

        public void LockDoor();
        public void UnLockDoor();
    }
}
