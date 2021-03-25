using System;
using System.Collections.Generic;
using System.Text;
using ChargingMonitor.Door;
using ChargingMonitor.RFIDReader;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;
using UsbSimulator;

namespace ChargingMonitor.Test.Unit
{
    public class TestStationControl_Door
    {
        private StationControl _uut;
        private IDoor door;

        [SetUp]
        public void Setup()
        {
            door = Substitute.For<IDoor>();
            _uut = new StationControl(door, new ChargeControl(new UsbChargerSimulator()), new RfidReader(),
                new Display(), new LogFiles.Log());
        }

    }
}
