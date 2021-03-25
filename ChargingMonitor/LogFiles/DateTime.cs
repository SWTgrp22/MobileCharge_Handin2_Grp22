using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingMonitor.LogFiles
{
    public class DateTime:IDateTime
    {
        public string timeStamp()
        {
            string result = System.DateTime.Now.ToString();
            return result;
        }
    }
}
