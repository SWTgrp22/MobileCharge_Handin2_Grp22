using System;

namespace ChargingMonitor.Display
{
    public class ChargeDisplay : IDisplay
    {
        public void ShowMessage(string meassage)
        {
            Console.WriteLine(meassage);
        }
    }
}