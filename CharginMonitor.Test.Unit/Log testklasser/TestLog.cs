using System;
using System.Collections.Generic;
using System.Text;
using ChargingMonitor.LogFiles;
using NSubstitute;
using NUnit.Framework;

namespace ChargingMonitor.Test.Unit.Log_testklasser
{
    public class TestLog
    {
        private Log _uut;
        private IWriter fileWriter;
        private IDateTime dateTime;
        [SetUp]
        public void SetUp()
        {
            fileWriter = Substitute.For<IWriter>();
            dateTime = Substitute.For<IDateTime>();

            _uut = new Log(fileWriter,dateTime);
        }

        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        public void LogDoorUnlock_DoorUnLockedLogged_FileWriterRecivesACall(int ID)
        {
            _uut.LogDoorUnLocked(ID);

            fileWriter.Received(1).Write("", ": Skab låst op med RFID: ", ID);
        }

        [TestCase(2)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        public void LogDoorUnlock_DoorLockedLogged_FileWriterRecivesACall(int ID)
        {
            _uut.LogDoorLocked(ID);

            fileWriter.Received(1).Write("", ": Skab låst med RFID: ", ID);
        }
    }
}
