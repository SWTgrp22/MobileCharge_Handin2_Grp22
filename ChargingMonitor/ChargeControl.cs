using System;
using UsbSimulator;

namespace ChargingMonitor
{
    public class ChargeControl : IChargeControl
    {
        public bool Connected { get; set; }
        private IUsbCharger usbCharger;
        public double CurrentCurrent;

        public ChargeControl(IUsbCharger usbCharger)
        {
            this.usbCharger = usbCharger;
            usbCharger.CurrentValueEvent += HandleUSBChargeEvent;// det der svarer til den gamle attach()
        }

        public void StartCharge()
        {
            usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            usbCharger.StopCharge();
        }

        private void HandleUSBChargeEvent(object s, CurrentEventArgs e)
        {
            CurrentCurrent = e.Current;

            if (CurrentCurrent > 0 && CurrentCurrent <= 5)
            {
                usbCharger.StopCharge();
                Console.WriteLine("Telefonen er fuldt opladt");
                Connected = true;
            }
            else if (CurrentCurrent > 5 && CurrentCurrent <= 500)
            {
                Console.WriteLine("Oplader telefon...");
                Connected = true;
            }
            else if (CurrentCurrent > 500)
            {
                usbCharger.StopCharge();
                Console.WriteLine("Error..."); 
                Connected = true;
            }
            else if (CurrentCurrent ==0)
            {
                Connected = false;
            } 
        }
    }
}
