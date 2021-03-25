using System;

namespace ChargingMonitor.LogFiles
{
    public class Log : ILog
    {
        private FileWriter writer = new FileWriter("Logfile.txt");
        private DateTime timeStamp = new DateTime();

        public void LogDoorLocked(int ID)
        {
            writer.Write(timeStamp.timeStamp(), ": Skab låst med RFID: ", ID);
        }

        public void LogDoorUnLocked(int ID)
        {
            writer.Write(timeStamp.timeStamp(), ": Skab låst op med RFID: ", ID);
        }
    }
}