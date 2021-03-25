using System;
using System.Collections.Generic;
using System.Text;
using ChargingMonitor.Door;
using ChargingMonitor.LogFiles;
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

        [Test]
        public void DoorStatusChanged_doorIsOpenIsTrue_CorrectMessageWasSend()
        {
            door.doorChangedEvent += Raise.EventWith(new DoorEventArg {doorIsopen = true});

            var expectedState = Ladeskab.StationControl.LadeskabState.DoorOpen;

            //Da Avaiable er default værdi for enum Ladeskab testes der for hvilken besked der sendes til display
            Assert.Multiple(() =>
            {
                Assert.That(_uut._state, Is.EqualTo(expectedState));
                Assert.That(_uut.message, Is.EqualTo("Tilslut telefon"));
            });
        }

        [Test]
        public void DoorStatusChanged_doorIsOpenIsFalse_CorrectMessageWasSend()
        {
            door.doorChangedEvent += Raise.EventWith(new DoorEventArg { doorIsopen = false });

            var expectedState = Ladeskab.StationControl.LadeskabState.DoorOpen;

            //Da Avaiable er default værdi for enum Ladeskab testes der for hvilken besked der sendes til display
            Assert.That(_uut.message, Is.EqualTo("Hold dit RFID tag op til scanneren"));
           
        }

    }
}
