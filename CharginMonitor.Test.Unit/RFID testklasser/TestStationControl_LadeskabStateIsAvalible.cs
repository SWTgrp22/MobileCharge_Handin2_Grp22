using System;
using System.Collections.Generic;
using System.Text;
using ChargingMonitor.Display;
using ChargingMonitor.Door;
using ChargingMonitor.LogFiles;
using ChargingMonitor.RFIDReader;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;
using UsbSimulator;

namespace ChargingMonitor.Test.Unit.RFID_testklasser
{
    public class TestStationControl_LadeskabStateIsAvalible
    {
        private StationControl _uut;
        private IRFIDReader rfidReader;
        private IDoor door;
        private IChargeControl chargeControl;
        private IDisplay display;
        private ILog log;

        [SetUp]
        public void Setup()
        {
            rfidReader = Substitute.For<IRFIDReader>();
            door = Substitute.For<IDoor>();
            chargeControl = Substitute.For<IChargeControl>();
            display = Substitute.For<IDisplay>();
            log = Substitute.For<ILog>();
            _uut = new StationControl(door, chargeControl, rfidReader,
                display, log);
        }

        [Test]
        public void RfidDeected_LadeskabstateIsAvalibleAndChargerIsConnected_CalsLockDoor()
        {
            //Act
            chargeControl.Connected = true;

            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = 21});

            door.Received(1).LockDoor();
        }

        [Test]
        public void RfidDeected_LadeskabstateIsAvalibleAndChargerIsConnected_CalsStartCharge()
        {
            //Act
            chargeControl.Connected = true;

            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = 21 });

            chargeControl.Received(1).StartCharge();
        }

        [TestCase(21)]
        public void RfidDeected_LadeskabstateIsAvalibleAndChargerIsConnected_CalsLogDoorLocked(int newId)
        {
            //Act
            chargeControl.Connected = true;

            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });

            log.Received(1).LogDoorLocked(newId);
        }

        [Test]
        public void RfidDeected_LadeskabstateIsAvalibleAndChargerIsConnected_StateChangeToLocked()
        {
            //Act
            chargeControl.Connected = true;

            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = 21 });

            var expectedState = Ladeskab.StationControl.LadeskabState.Locked;

            //Assert
            Assert.That(_uut._state, Is.EqualTo(expectedState));
        }


        //TODO Her skifter vi til at teste uden telefonen er tilsuttet 

        [Test]
        public void RfidDeected_LadeskabstateIsAvalibleAndChargerIsNOTConnected_DoesNotCalLockDoor()
        {
            //Act
            chargeControl.Connected = false;
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = 21 });

            door.Received(0).LockDoor();
        }

        [Test]
        public void RfidDeected_LadeskabstateIsAvalibleAndChargerIsNOTConnected_DoesNotCalStartCharge()
        {
            //Act
            chargeControl.Connected = false;
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = 21 });

            chargeControl.Received(0).StartCharge();
        }

        [TestCase(21)]
        public void RfidDeected_LadeskabstateIsAvalibleAndChargerIsNOTConnected_DoesNotCalLogDoorLocked(int newId)
        {
            //Act
            chargeControl.Connected = false;
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });

            log.Received(0).LogDoorLocked(newId);
        }

        [Test]
        public void RfidDeected_LadeskabstateIsAvalibleAndChargerIsNOTConnected_CalsDisplayShowMessage()
        {
            //Act
           
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = 21 });

            display.Received(1).ShowMessage("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }

    }
}
