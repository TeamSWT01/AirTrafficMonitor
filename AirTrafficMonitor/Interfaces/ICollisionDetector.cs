using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Implementation;

namespace AirTrafficMonitor.Interfaces
{
    public interface ICollisionDetector
    {
        IWriter ConsoleWriter { get; set; }
        IWriter LogWriter { get; set; }

        void DetectCollision(List<ITrack> tracks);
    }
}
