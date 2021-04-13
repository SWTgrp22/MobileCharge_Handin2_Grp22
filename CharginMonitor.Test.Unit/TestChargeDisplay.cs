using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using NUnit.Framework;

namespace ChargingMonitor.Test.Unit
{
    public class TestChargeDisplay
    {
        private Display.ChargeDisplay _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new Display.ChargeDisplay();
        }

        [TestCase("Telefonen er fuldt opladt")]
        [TestCase("Oplader telefon...")]
        [TestCase("Error...")]
        public void ShowMeassage_MethodInputIsWrittenToConsole(string meassage)
        {
            //Act
            // setup test - redirect Console.Out
            var sw = new StringWriter();
            Console.SetOut(sw);

            // exercise system under test
            _uut.ShowMessage(meassage);//skriver til consollen

            // verify
            Assert.AreEqual(meassage + "\r\n", sw.ToString());//Tjekker på at det der blev taget fra consollen er lig beskeden som blev sendt med ShowMessage()

            //Har fundet det på følgende hjemmeside...
            //https://stackoverflow.com/questions/30577603/how-do-i-convert-console-output-to-a-string
        }
    }
}
