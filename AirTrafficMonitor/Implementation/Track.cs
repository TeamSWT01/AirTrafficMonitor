using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Interfaces;

namespace AirTrafficMonitor.Implementation
{
    public class Track : ITrack
    {
        public string Tag { get; set; }
        public double Velocity { get; set; }
        public int Course { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Altitude { get; set; }
        public DateTime TimeStamp { get; set; }

        public int CalculateCourse()
        {
            throw new NotImplementedException();
        }
    }
}
