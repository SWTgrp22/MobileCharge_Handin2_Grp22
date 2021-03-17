using System;
using System.Collections.Generic;
using System.Text;

namespace ChargingMonitor
{
    public interface IChargeControl
    {
        public bool Connected { get; set; }
        public void StartCharge();
        public void StopCharge();
    }
}
