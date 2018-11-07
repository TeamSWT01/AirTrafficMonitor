using System;

namespace AirTrafficMonitor.Interfaces
{
    public interface ITrack
    {
        string Tag { get; set; }
        double Velocity { get; set; }
        int Course { get; set; }
        int X { get; set; }
        int Y { get; set; }
        int Altitude { get; set; }
        DateTime TimeStamp { get; set; }

        void CalculateCourse(ITrack oldTrack, ITrack newTrack);
        void CalculateVelocity(ITrack oldTrack, ITrack newTrack);
    }
}
