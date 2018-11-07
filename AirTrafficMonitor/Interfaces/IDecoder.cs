using System;
using System.Collections.Generic;
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
        Action<List<ITrack>> OnTracksReady { get;  set; }
        Action<ITrack> TrackEntered { get; set; }
        Action<ITrack> TrackLeaving { get; set; }

        void DecodeTransData(object sender, RawTransponderDataEventArgs e);
    }
}
