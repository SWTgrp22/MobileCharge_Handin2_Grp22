using System;

namespace ChargingMonitor.RFIDReader
{
    public class RfidReader : IRFIDReader
    {
        public event EventHandler<RFIDReaderEventArg> RFIDReaderEvent;
        private int ID;

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void SimulateReadID()
        {

        }

        protected virtual void RFIDDetected(RFIDReaderEventArg e)
        {
            RFIDReaderEvent?.Invoke(this, e);
        }
    }
}