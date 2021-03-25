using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingMonitor.LogFiles
{
    public interface IWriter
    {
        void Write(string timeStamp, string s, int id);
    }
}
