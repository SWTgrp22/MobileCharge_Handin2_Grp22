using System;

namespace ChargingMonitor
{
    public class ChargeControl : IChargeControl
    {
        public bool Connected { get; set; }
        public void StartCharge()
        {
            throw new NotImplementedException();
        }

        public void StopCharge()
        {
            throw new NotImplementedException();
        }
    }
}
