using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChargingMonitor.LogFiles
{
    public class FileWriter
    {
        private string _filename;

        public FileWriter(string filename)
        {
            _filename = filename;

        }

        public void Write(string dateTime, string s, int id)
        {
            using (var writer = File.AppendText(_filename))
            {
                writer.WriteLine(dateTime + s + id);
            }
        }
    }
}
