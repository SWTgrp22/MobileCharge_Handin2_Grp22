using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingMonitor.RFIDReader
{
    public interface IRFIDReader
    {
        public event EventHandler<RFIDReaderEventArg> RFIDReaderEvent;

        public void Read();
    }
}
