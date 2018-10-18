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
        void DetectCollision(List<ITrack> Tracks);
    }
}
