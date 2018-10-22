using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitor.Interfaces
{
    public interface IDecoder
    {
        ITransponderReceiver TransponderReceiver { get; set; }
        IWriter Writer { get; set; }
        ICollisionDetector CollisionDetector { get; set; }
        List<ITrack> Tracks { get; set; }
        List<ITrack> OldTracks { get; set; }
        ITrack Track { get; set; }

        void DecodeTransData(object sender, RawTransponderDataEventArgs e);
    }
}
