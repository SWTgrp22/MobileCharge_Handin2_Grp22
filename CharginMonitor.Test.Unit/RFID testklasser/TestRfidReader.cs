using System;
using System.Collections.Generic;
using System.Text;
using ChargingMonitor.RFIDReader;
using NUnit.Framework;

namespace ChargingMonitor.Test.Unit
{
    public class TestRfidReader
    {
        private RfidReader _uut; 
        private RFIDReaderEventArg _recevedEventArg;

        [SetUp]
        public void Setup()
        {
            _recevedEventArg = null;

            _uut = new RfidReader();

            //Set up an event listener to chek the event occerence and event data
            _uut.RFIDReaderEvent +=
                (o, args) =>
                {
                    _recevedEventArg = args;
                };

        }

        [Test]
        public void SimulateDetection_Detected_IsEqualToTrue()
        {
            //Act
            _uut.SimulateDetection();

            //Assert
            Assert.That(_uut.Detected, Is.True);

        }


        [Test]
        public void RfidDetected_NonRfidTagDetected_EventNotFired()
        {
            //Act
            _uut.RfidDetected(0);


            //Assert
            Assert.That(_recevedEventArg, Is.Null);
        }

        [Test]
        public void RfidDetected_RfidTagDetected_EventFired()
        {
            //Act
            _uut.SimulateDetection();
            _uut.RfidDetected(0);


            //Assert
            Assert.That(_recevedEventArg, Is.Not.Null);
        }

        [TestCase(0)]
        [TestCase(210)]
        [TestCase(555555)]
        [TestCase(2147483647)]
        public void RfidDetected_RfidTagDetected_EventIdIsEqualtoId(int id)
        {
            //Act
            _uut.SimulateDetection();
            _uut.RfidDetected(id);


            //Assert
            Assert.That(_recevedEventArg.ID, Is.EqualTo(id));
        }

    }
}
