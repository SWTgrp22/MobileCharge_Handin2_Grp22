using System;
using System.Collections.Generic;
using System.Text;
using ChargingMonitor.Display;
using ChargingMonitor.Door;
using ChargingMonitor.RFIDReader;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;

namespace ChargingMonitor.Test.Unit.RFID_testklasser
{
    public class TestStationControl_LadeskabIsLocked
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

        [TestCase(0)]
        [TestCase(210)]
        [TestCase(555555)]
        [TestCase(2147483647)]
        public void RfidDeected_LadeskabstateIsLockedAndIdIsEqualToOldId_CalsStopCharge(int newId)
        {
            //Act
            //Først fyrres eventet en gang for at skifte state fra avalible til Locked
            chargeControl.Connected = true;
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });

            //Herefter fyrres eventet igen
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });


            chargeControl.Received(1).StopCharge();
        }


        [TestCase(0)]
        [TestCase(210)]
        [TestCase(555555)]
        [TestCase(2147483647)]
        public void RfidDeected_LadeskabstateIsLockedAndIdIsEqualToOldId_CalsUnLockDoor(int newId)
        {
            //Act
            //Først fyrres eventet en gang for at skifte state fra avalible til Locked
            chargeControl.Connected = true;
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId});

            //Herefter fyrres eventet igen
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });


            door.Received(1).UnLockDoor();
        }

        [TestCase(0)]
        [TestCase(210)]
        [TestCase(555555)]
        [TestCase(2147483647)]
        public void RfidDeected_LadeskabstateIsLockedAndIdIsEqualToOldId_CalsLogUnLockedDoor(int newId)
        {
            //Act
            //Først fyrres eventet en gang for at skifte state fra avalible til Locked
            chargeControl.Connected = true;
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });

            //Herefter fyrres eventet igen
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });


            log.Received(1).LogDoorUnLocked(newId);
        }

        [TestCase(0)]
        [TestCase(210)]
        [TestCase(555555)]
        [TestCase(2147483647)]
        public void RfidDeected_LadeskabstateIsLockedAndIdIsEqualToOldId_CalsDisplay(int newId)
        {
            //Act
            //Først fyrres eventet en gang for at skifte state fra avalible til Locked
            chargeControl.Connected = true;
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });

            //Herefter fyrres eventet igen
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });


            display.Received(1).ShowMessage("Tag din telefon ud af skabet og luk døren");
        }

        [TestCase(0)]
        [TestCase(210)]
        [TestCase(555555)]
        [TestCase(2147483647)]
        public void RfidDeected_LadeskabstateIsLockedAndIdIsEqualToOldId_StateIsAvalible(int newId)
        {
            //Act
            //Først fyrres eventet en gang for at skifte state fra avalible til Locked
            chargeControl.Connected = true;
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });

            //Herefter fyrres eventet igen
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });

            var expectedState = Ladeskab.StationControl.LadeskabState.Available;

            //Assert
            Assert.That(_uut._state, Is.EqualTo(expectedState));
        }


        //TODO Her detectes et rfid der ikke stemmer overens med det der blev benyttet til at låse skabet

        [TestCase(0, 1)]
        [TestCase(210,211)]
        [TestCase(555555,6666666)]
        [TestCase(2147483647, 2147483646)]
        public void RfidDeected_LadeskabstateIsLockedAndIdIsNOTEqualToOldId_DoesNotCalStopCharge(int oldId, int newId)
        {
            //Act
            //Først fyrres eventet en gang for at skifte state fra avalible til Locked
            chargeControl.Connected = true;
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = oldId });

            //Herefter fyrres eventet igen
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });

            chargeControl.Received(0).StopCharge();
        }



        [TestCase(0, 1)]
        [TestCase(210, 211)]
        [TestCase(555555, 6666666)]
        [TestCase(2147483647, 2147483646)]
        public void RfidDeected_LadeskabstateIsLockedAndIdIsNOTEqualToOldId_DoesNotCalUnLockDoor(int oldId, int newId)
        {
            //Act
            //Først fyrres eventet en gang for at skifte state fra avalible til Locked
            chargeControl.Connected = true;
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = oldId });
           

            //Herefter fyrres eventet igen
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });


            door.Received(0).UnLockDoor();
        }

        [TestCase(0, 1)]
        [TestCase(210, 211)]
        [TestCase(555555, 6666666)]
        [TestCase(2147483647, 2147483646)]
        public void RfidDeected_LadeskabstateIsLockedAndIdIsNOTEqualToOldId_DoesNotCalLogUnLockedDoor(int oldId, int newId)
        {
            //Act
            //Først fyrres eventet en gang for at skifte state fra avalible til Locked
            chargeControl.Connected = true;
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = oldId });
           
            //Herefter fyrres eventet igen
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });


            log.Received(0).LogDoorUnLocked(newId);
        }

         [TestCase(0, 1)]
        [TestCase(210, 211)]
        [TestCase(555555, 6666666)]
        [TestCase(2147483647, 2147483646)]
        public void RfidDeected_LadeskabstateIsLockedAndIdIsNOTEqualToOldId_CalsDisplay(int oldId, int newId)
        {
            //Act
            //Først fyrres eventet en gang for at skifte state fra avalible til Locked
            chargeControl.Connected = true;
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = oldId });

            //Herefter fyrres eventet igen
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });


            display.Received(1).ShowMessage("Forkert RFID tag");
        }



    }
}
