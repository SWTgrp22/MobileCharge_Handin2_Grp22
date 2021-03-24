using ChargingMonitor;
using NSubstitute;
using NUnit.Framework;
using UsbSimulator;

namespace CharginMonitor.Test.Unit
{
    public class Tests
    {
        private ChargeControl _uut;
        private IUsbCharger _usbCharger;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _usbCharger = Substitute.For<IUsbCharger>();
            _uut = new ChargeControl(_usbCharger);
        }

        [TestCase(0.1)]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(499)]
        [TestCase(500)]
        [TestCase(501)]
        public void ChargingCurrentChanged_ConnectedArguments_ConnectedIsTrue(double newCurrent)//State based test
        {
            //Act
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs {Current = newCurrent});

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_uut.CurrentCurrent, Is.EqualTo(newCurrent));
                Assert.That(_uut.Connected, Is.True);
            });
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void ChargingCurrentChanged_DisConnectedArguments_ConnectedIsFalse(double newCurrent)
        {
            //Act
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = newCurrent });

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(_uut.CurrentCurrent, Is.EqualTo(newCurrent));
                Assert.That(_uut.Connected, Is.False);
            });
        }

        [TestCase(0)]
        [TestCase(6)]
        [TestCase(499)]
        [TestCase(500)]
        public void ChargingCurrentChanged_ContinueChargingArguments_ChargeControlHasNotInteracted(double newCurrent)//Interaction test
        {
            //Act
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = newCurrent });

            //Assert
            _usbCharger.Received(0).StopCharge();
        }

        [TestCase(0.1)]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(501)]
        public void ChargingCurrentChanged_StopChargingArguments_ChargeControlHasInteracted(double newCurrent)//Interaction test
        {
            //Act
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = newCurrent });
            
            //Assert
            _usbCharger.Received(1).StopCharge();
        }

        [Test]
        public void StartChargeIsCalledInUUT_StartChargeIsCalledInSUB()
        {
            //Act
            _uut.StartCharge();

            //Assert
            _usbCharger.Received(1).StartCharge();
        }

        [Test]
        public void StopChargeIsCalledInUUT_StopChargeIsCalledInSUB()
        {
            //Act
            _uut.StopCharge();

            //Assert
            _usbCharger.Received(1).StopCharge();
        }
    }
}