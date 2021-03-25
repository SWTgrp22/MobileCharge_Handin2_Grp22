using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingMonitor.LogFiles
{
    public class DateTime
    {
        public string timeStamp()
        {
            string result = System.DateTime.Now.ToString();
            return result;
        }
    }
}
