using System;

namespace ChargingMonitor.RFIDReader
{
    public class RfidReader : IRFIDReader
    {
        public event EventHandler<RFIDReaderEventArg> RFIDReaderEvent;
        public bool Detected { get; private set; } = false;

        public void RfidDetected(int id)
        {
            if (Detected)
            {
                DetectedRfidTag(new RFIDReaderEventArg(){ID = id});
                Detected = false;
            }

        }

        public void SimulateDetection()
        {
            Detected = true;
        }

        protected virtual void DetectedRfidTag(RFIDReaderEventArg e)
        {
            RFIDReaderEvent?.Invoke(this, e);
        }
    }
}