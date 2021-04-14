using System;

namespace ChargingMonitor.LogFiles
{
    public class Log : ILog
    {
        private IWriter writer;
        private IDateTime timeStamp;
        public string dateRecived { get; set; } = "";

        public Log(IWriter writer, IDateTime timeStamp)
        {
            this.writer = writer;
            this.timeStamp = timeStamp;
        }
        public void LogDoorLocked(int ID)
        {
            dateRecived = timeStamp.timeStamp();
            writer.Write(dateRecived, ": Skab låst med RFID: ", ID);
        }

        public void LogDoorUnLocked(int ID)
        {
            dateRecived = timeStamp.timeStamp();
            writer.Write(dateRecived, ": Skab låst op med RFID: ", ID);
        }
    }
}