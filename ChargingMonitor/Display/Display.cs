using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingMonitor.Display
{
    public class Display : IDisplay
    {

        public void ShowMessage(string meassage)
        {
            Console.WriteLine(meassage);
        }
    }
}
