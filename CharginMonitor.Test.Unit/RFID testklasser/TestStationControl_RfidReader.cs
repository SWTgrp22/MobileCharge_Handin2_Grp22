using System;
using System.Collections.Generic;
using System.Text;
using ChargingMonitor.LogFiles;
using ChargingMonitor.RFIDReader;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;
using UsbSimulator;

namespace ChargingMonitor.Test.Unit.RFID_testklasser
{
    public class TestStationControl_RfidReader
    {
        private StationControl _uut;
        private IRFIDReader rfidReader;

        [SetUp]
        public void Setup()
        {
            rfidReader = Substitute.For<IRFIDReader>();
            _uut = new StationControl(new Door.Door(), new ChargeControl(new UsbChargerSimulator()), rfidReader,
                new Display.Display(), new Log());
        }

        [TestCase(0)]
        [TestCase(210)]
        [TestCase(555555)]
        [TestCase(2147483647)]
        public void RfidDetected_DetectedTag_IDIsEqualToID(int newId)
        {
            //Act
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });

            //Assert
            Assert.That(_uut._rfidID, Is.EqualTo(newId));

        }


    }
}
