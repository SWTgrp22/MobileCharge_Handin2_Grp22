using System;
using System.Collections.Generic;
using System.Text;
using ChargingMonitor.Display;
using ChargingMonitor.Door;
using ChargingMonitor.RFIDReader;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;

namespace ChargingMonitor.Test.Unit
{
    public class TestStationControl
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

        #region Ladeskab Is Locked
        
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

        #endregion

        #region Ladeskab State Is Avalible

        [Test]
        public void RfidDeected_LadeskabstateIsAvalibleAndChargerIsConnected_CallsLockDoor()
        {
            //Act
            chargeControl.Connected = true;

            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = 21 });

            door.Received(1).LockDoor();
        }

        [Test]
        public void RfidDeected_LadeskabstateIsAvalibleAndChargerIsConnected_CallsStartCharge()
        {
            //Act
            chargeControl.Connected = true;

            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = 21 });

            chargeControl.Received(1).StartCharge();
        }

        [TestCase(21)]
        public void RfidDeected_LadeskabstateIsAvalibleAndChargerIsConnected_CallsLogDoorLocked(int newId)
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

            //Assert - denne bør ændres til at være mere blackbox fremfor white box, men vi kunne ikke lige overskue hvordan:
            Assert.That(_uut._state, Is.EqualTo(expectedState));
        }


        //TODO Her skifter vi til at teste uden telefonen er tilsuttet 

        [Test]
        public void RfidDeected_LadeskabstateIsAvalibleAndChargerIsNOTConnected_DoesNotCallLockDoor()
        {
            //Act
            chargeControl.Connected = false;
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = 21 });

            //Assert
            door.Received(0).LockDoor();
        }

        [Test]
        public void RfidDeected_LadeskabstateIsAvalibleAndChargerIsNOTConnected_DoesNotCallStartCharge()
        {
            //Act
            chargeControl.Connected = false;
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = 21 });

            //Assert
            chargeControl.Received(0).StartCharge();
        }

        [TestCase(21)]
        public void RfidDeected_LadeskabstateIsAvalibleAndChargerIsNOTConnected_DoesNotCallLogDoorLocked(int newId)
        {
            //Act
            chargeControl.Connected = false;
            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });

            //Assert
            log.Received(0).LogDoorLocked(newId);
        }

        [Test]
        public void RfidDeected_LadeskabstateIsAvalibleAndChargerIsNOTConnected_CallsDisplayShowMessage()
        {
            //Act

            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = 21 });

            //Assert
            display.Received(1).ShowMessage("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }
        #endregion

        #region Rfid detected
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

        [TestCase(0)]
        [TestCase(210)]
        [TestCase(555555)]
        [TestCase(2147483647)]
        public void RfidDetected_DetectedTag_StateChangesToAviable(int newId)
        {
            //Act
            door.doorChangedEvent += Raise.EventWith(new DoorEventArg { doorIsopen = true });

            rfidReader.RFIDReaderEvent += Raise.EventWith(new RFIDReaderEventArg { ID = newId });

            //Assert
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Available));
        }
        #endregion
        
        #region Display call
        [Test]
        public void DoorStatusChanged_doorIsOpenIsTrue_CorrectMessageWasReceived()
        {
            door.doorChangedEvent += Raise.EventWith(new DoorEventArg { doorIsopen = true });
            
            //Assert
            display.Received().ShowMessage("Tilslut telefon");
        }

        [Test]
        public void DoorStatusChanged_doorIsOpenIsFalse_CorrectMessageWasReceived()
        {
            door.doorChangedEvent += Raise.EventWith(new DoorEventArg { doorIsopen = false });

            //Assert
            display.Received().ShowMessage("Hold dit RFID tag op til scanneren");
        }
        
        #endregion

    }
}
