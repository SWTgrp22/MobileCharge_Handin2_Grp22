using System;
using ChargingMonitor.Display;
using UsbSimulator;

namespace ChargingMonitor
{
    public class ChargeControl : IChargeControl
    {
        public bool Connected { get; set; }
        private IUsbCharger usbCharger;
        public double CurrentCurrent;
        public IDisplay _Display;

        public ChargeControl(IUsbCharger usbCharger, IDisplay display)
        {
            _Display = display;
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
                _Display.ShowMessage("Telefonen er fuldt opladt");
                Connected = true;
            }
            else if (CurrentCurrent > 5 && CurrentCurrent <= 500)
            {
                _Display.ShowMessage("Oplader telefon...");
                Connected = true;
            }
            else if (CurrentCurrent > 500)
            {
                usbCharger.StopCharge();
                _Display.ShowMessage("Error..."); 
                Connected = true;
            }
            else if (CurrentCurrent ==0)
            {
                Connected = false;
            } 
        }
    }
}
