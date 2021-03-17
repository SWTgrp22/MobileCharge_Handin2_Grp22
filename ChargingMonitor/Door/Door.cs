using System;

namespace ChargingMonitor.Door
{
    public class Door : IDoor
    {
        public event EventHandler<DoorEventArg> doorChangedEvent;
        private bool IsDoorOpen = false;
        private bool oldDoorState = false;
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
            if (IsDoorOpen != oldDoorState)
            {
                DoorChangedState(new DoorEventArg{doorIsopen = true});
                oldDoorState = IsDoorOpen;
            }
        }

        public void SimulateDoorCloses()
        {
            IsDoorOpen = false;
            if (IsDoorOpen != oldDoorState)
            {
                DoorChangedState(new DoorEventArg { doorIsopen = false });
                oldDoorState = IsDoorOpen;
            }
        }

        protected virtual void DoorChangedState(DoorEventArg e)
        {
            doorChangedEvent?.Invoke(this,e);
        }
    }
}