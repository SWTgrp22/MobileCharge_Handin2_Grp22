using System;
using System.Collections.Generic;
using System.Text;
using ChargingMonitor.LogFiles;
using NSubstitute;
using NUnit.Framework;

namespace ChargingMonitor.Test.Unit
{
    public class TestLog
    {
        private LogFiles.Log _uut;
        private IWriter fileWriter;
        private IDateTime dateTime;

        [SetUp]
        public void SetUp()
        {
            fileWriter = Substitute.For<IWriter>();
            dateTime = Substitute.For<IDateTime>();

            _uut = new LogFiles.Log(fileWriter,dateTime);
        }

        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]//Forskellige ID-inputs
        public void LogDoorUnlock_DoorUnLockedLogged_FileWriterDateTimeRecivesACall(int ID)
        {
            _uut.LogDoorUnLocked(ID);

            Assert.Multiple(() =>
            {
                dateTime.Received(1).timeStamp();
                fileWriter.Received(1).Write("", ": Skab låst op med RFID: ", ID);
            });
        }

        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]//Forskellige ID-inputs
        public void LogDoorUnlock_DoorLockedLogged_FileWriterDateTimeRecivesACall(int ID)
        {
            _uut.LogDoorLocked(ID);

            Assert.Multiple(() =>
            {
                dateTime.Received(1).timeStamp();
                fileWriter.Received(1).Write("", ": Skab låst med RFID: ", ID);
            });
        }

        [TestCase("DATO",21)]//Forskellige timeStamps
        [TestCase("November", 21)]
        [TestCase("September", 21)]
        [TestCase("Januar", 21)]
        [TestCase("Februar", 21)]
        public void LogDoorUnlock_DoorLockedLogged_DateTimeReturnsCorrect(string message, int id)
        {
            //Act
            dateTime.timeStamp().Returns(message);
            _uut.LogDoorLocked(id);
            
            //Assert
            Assert.That(_uut.dateRecived,Is.EqualTo(message));
        }
    }
}
