using System;

namespace ChargingMonitor.RFIDReader
{
    public class RfidReader : IRFIDReader
    {
        public event EventHandler<RFIDReaderEventArg> RFIDReaderEvent;
        public bool Detected { get; private set; }
        
        private int _readId;

        public void RfidDetected(int id)
        {
            _readId = id;

            if (Detected)
            {
                DetectedRfidTag(new RFIDReaderEventArg(){ID = _readId});
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