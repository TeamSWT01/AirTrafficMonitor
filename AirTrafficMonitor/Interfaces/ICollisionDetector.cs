using System.Collections.Generic;

namespace AirTrafficMonitor.Interfaces
{
    public interface ICollisionDetector
    {
        IWriter ConsoleWriter { get; set; }
        IWriter LogWriter { get; set; }

        void DetectCollision(List<ITrack> tracks);
    }
}
