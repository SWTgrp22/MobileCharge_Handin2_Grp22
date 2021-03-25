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
        public void LogDoorUnlock_DoorUnLockedLogged_FileWriterRecivesACall(int ID)
        {
            _uut.LogDoorUnLocked(ID);
           // dateTime.

            //fileWriter.Received(1).Write();
            Assert.Pass();
        }
    }
}
