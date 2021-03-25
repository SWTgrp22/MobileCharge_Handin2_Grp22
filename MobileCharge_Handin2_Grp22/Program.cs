using System;
using ChargingMonitor;
using ChargingMonitor.Display;
using ChargingMonitor.Door;
using ChargingMonitor.LogFiles;
using ChargingMonitor.RFIDReader;
using Ladeskab;
using UsbSimulator;

namespace MobileCharge_Handin2_Grp22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Assemble your system here from all the classes
            var door = new Door();
            IUsbCharger usbCharger = new UsbChargerSimulator();
            var charger = new ChargeControl(usbCharger);
            var reader = new RfidReader();
            var display = new Display();
            var log = new Log();

            var stationControl = new StationControl(door, charger, reader, display, log);

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R, T: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.SimulateDoorOpens();
                        break;

                    case 'C':
                        door.SimulateDoorCloses();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        reader.SimulateDetection();
                        reader.RfidDetected(id);
                        break;

                    case 'T':// Tilføjet for at kunne "Connected" telefonen til opladeren.
                        charger.Connected = true;
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
