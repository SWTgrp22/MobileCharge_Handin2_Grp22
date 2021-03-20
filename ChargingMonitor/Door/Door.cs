using System;

namespace ChargingMonitor.Door
{
    public class Door : IDoor
    {
        public event EventHandler<DoorEventArg> doorChangedEvent;
        private bool isDoorLocked = false;
        private bool IsDoorOpen = false;
        private bool oldDoorState = false;
        public void LockDoor()
        {
            isDoorLocked = true;
        }

        public void UnLockDoor()
        {
            isDoorLocked = false;
        }

        public void SimulateDoorOpens()
        {
            if (isDoorLocked == false)// tjekker på om døren er låst eller ej, måske det er irrelevant men så har (un)lock() en funktion...
            {
                IsDoorOpen = true;

                if (IsDoorOpen != oldDoorState)
                {
                    DoorChangedState(new DoorEventArg { doorIsopen = true });
                    oldDoorState = IsDoorOpen;
                }
            }
            else
            {
                Console.WriteLine("Døren er låst...");// hvis døren er låst kan den ikke åbnes
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