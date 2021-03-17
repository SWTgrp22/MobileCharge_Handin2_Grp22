using System;

namespace ChargingMonitor.Door
{
    public class Door : IDoor
    {
        public event EventHandler<DoorEventArg> doorChangedEvent;
        private bool IsDoorOpen = false;
        public void LockDoor()
        {
            throw new NotImplementedException();
        }

        public void UnLockDoor()
        {
            throw new NotImplementedException();
        }

        public void SimulateDoorOpens()
        {
            IsDoorOpen = true;
        }

        public void SimulateDoorCloses()
        {
            IsDoorOpen = false;
        }
    }
}