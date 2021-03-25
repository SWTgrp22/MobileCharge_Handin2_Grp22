using System;

namespace ChargingMonitor.LogFiles
{
    public class Log : ILog
    {
        private IWriter writer;
            //= new FileWriter("Logfile.txt");
            private IDateTime timeStamp;
            //= new DateTime();

        public Log(IWriter writer, IDateTime timeStamp)
        {
            this.writer = writer;
            this.timeStamp = timeStamp;
        }

        public Log()
        {
        }

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