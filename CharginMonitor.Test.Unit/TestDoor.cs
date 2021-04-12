using System;
using System.Collections.Generic;
using System.Text;
using ChargingMonitor.Door;
using NUnit.Framework;

namespace ChargingMonitor.Test.Unit
{
    public class TestDoor
    {
        private Door.Door _uut;
        private DoorEventArg _recevedEventArg; 


        [SetUp]
        public void Setup()
        {
            _recevedEventArg = null; 

            _uut = new Door.Door();

            //Set up an event listener to chek the event occerence and event data
            _uut.doorChangedEvent +=
                (o, args) =>
                {
                    _recevedEventArg = args;
                };

        }

        [Test]
        public void SimulateDoorOpens_DoorIsUnlockedAndClosed_EventFired()
        {
            //Act
            _uut.UnLockDoor();

            _uut.SimulateDoorOpens();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_recevedEventArg, Is.Not.Null);
                Assert.That(_recevedEventArg.doorIsopen, Is.True);
            });

        }


        [Test]
        public void SimulateDoorOpens_DoorIsLockedAndClosed_EventNotFired()
        {
            //Act
            _uut.LockDoor();

            _uut.SimulateDoorOpens();

            //Assert
            Assert.That(_recevedEventArg, Is.Null);
        }

        [Test]
        public void SimulateDoorOpens_DoorIsAllreadyOpen_EventNotFired()
        {
            //Act
            _uut.UnLockDoor();
            _uut.oldDoorState = true;
            _uut.SimulateDoorOpens();

            //Assert
            Assert.That(_recevedEventArg, Is.Null);
        }

        [Test]
        public void SimulateDoorOpens_DoorIsLockedAndOpen_EventNotFired()
        {
            //Act
            _uut.LockDoor();
            _uut.oldDoorState = true;
            _uut.SimulateDoorOpens();

            //Assert
            Assert.That(_recevedEventArg, Is.Null);
        }

        [Test]
        public void SimulateDoorCloses_DoorIsOpen_EventFired()
        {
            //Act
            _uut.oldDoorState = true;
            _uut.SimulateDoorCloses();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_recevedEventArg, Is.Not.Null);
                Assert.That(_recevedEventArg.doorIsopen, Is.False);
            });

        }

        [Test]
        public void SimulateDoorCloses_DoorIsClosed_EventNotFired()
        {
            //Act
            _uut.oldDoorState = false;
            _uut.SimulateDoorCloses();

            //Assert
            Assert.That(_recevedEventArg, Is.Null);

        }

        [Test]
        public void LockDoor_IsDoorLocked_IsEqualToTrue()
        {
            //Act
            _uut.LockDoor();

            //Assert
            Assert.That(_uut.isDoorLocked,Is.True);

        }

        [Test]
        public void UnLockDoor_IsDoorLocked_IsEqualToFalse()
        {
            //Act
            _uut.UnLockDoor();

            //Assert
            Assert.That(_uut.isDoorLocked, Is.False);

        }


    }
}
