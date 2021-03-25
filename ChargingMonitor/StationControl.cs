using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingMonitor;
using ChargingMonitor.Door;
using ChargingMonitor.LogFiles;
using ChargingMonitor.RFIDReader;
using UsbSimulator;

namespace Ladeskab
{
    public class StationControl  
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        public LadeskabState _state { get; private set; } 
        private IChargeControl _charger;
        private int _oldId;
        private IDoor _door;
        private IRFIDReader _reader;
        private Display _display;
        public int _rfidID { get; private set; }
        private ILog _log;
        public string message = "";

        //Nu håber vi sgu lortet virker...
       // private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl(IDoor door, IChargeControl charger, IRFIDReader reader, Display display, ILog log)
        {
            _door = door;
            _charger = charger;
            _reader = reader;
            _display = display;
            _log = log;
            door.doorChangedEvent += HandleDoorEventArg; //Svarer til at attatce til eventet
            reader.RFIDReaderEvent += HandleRfidReaderEventArg;  //Svarer til at attatce til eventet

        }

        private void HandleDoorEventArg(object sender, DoorEventArg e)
        {
            if (e.doorIsopen)
            { 
                message = "Tilslut telefon";
              _display.ShowMessage(message);
              _state = LadeskabState.DoorOpen;
            }
            else
            {
                message = "Hold dit RFID tag op til scanneren";
               _display.ShowMessage(message);
                //RfidDetected();
                
            }
        }

        private void HandleRfidReaderEventArg(object sender, RFIDReaderEventArg e)
        {
            _rfidID = e.ID; 
            RfidDetected(_rfidID);
        }


        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                       _log.LogDoorLocked(id);

                        _display.ShowMessage("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.ShowMessage("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnLockDoor();
                        _log.LogDoorUnLocked(id);

                        _display.ShowMessage("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.ShowMessage("Forkert RFID tag");
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
    }
}
