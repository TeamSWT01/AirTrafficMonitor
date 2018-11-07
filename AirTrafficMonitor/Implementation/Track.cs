using System;
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

        public void CalculateCourse(ITrack oldTrack, ITrack newTrack)
        {
            int xdiff = oldTrack.X - newTrack.X;
            int ydiff = oldTrack.Y - newTrack.Y;
            Course = WithinUnitCircle(-(Math.Atan2(ydiff, xdiff) * 180.0 / Math.PI - 90.0));
        }

        private int WithinUnitCircle(double n)
        {
            double result = n % 360;
            if (n < 0)
                result += 360;
            return (int)result;
        }

        public void CalculateVelocity(ITrack oldTrack, ITrack newTrack)
        {
            int distance = (int) Math.Sqrt(Math.Pow(oldTrack.X - newTrack.X, 2) +
                                           Math.Pow(oldTrack.Y - newTrack.Y, 2));

            double timeDiff = (newTrack.TimeStamp - oldTrack.TimeStamp).TotalSeconds;

            Velocity = distance / timeDiff;
        }
    }
}
