using System.IO;
using AirTrafficMonitor.Interfaces;

namespace AirTrafficMonitor.Implementation
{
    public class LogWriter : IWriter
    {
        public void Write(string text)
        {
            string path = @"Track.txt";

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(text);
            }
        }
    }
}
