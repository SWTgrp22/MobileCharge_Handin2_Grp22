using System;

namespace ChargingMonitor.Door
{
    public class Door : IDoor
    {
        public event EventHandler<DoorEventArg> doorChangedEvent;
        public void LockDoor()
        {
            throw new NotImplementedException();
        }

        public void UnLockDoor()
        {
            throw new NotImplementedException();
        }
    }
}