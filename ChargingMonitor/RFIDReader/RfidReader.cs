using System;

namespace ChargingMonitor.RFIDReader
{
    public class RfidReader : IRFIDReader
    {
        public event EventHandler<RFIDReaderEventArg> RFIDReaderEvent;
        public void Read()
        {
            throw new NotImplementedException();
        }
    }
}