using System;
using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitor.Dto;
using AirTrafficMonitor.Interfaces;
using TransponderReceiver;

namespace AirTrafficMonitor.Implementation
{
    public class Decoder : IDecoder
    {
        public ITransponderReceiver TransponderReceiver { get; set; }
        public Airspace _airspace { get; set; }
        public IWriter Writer { get; set; }
        public ICollisionDetector CollisionDetector { get; set; }
        public List<ITrack> Tracks { get; set; }
        public List<ITrack> OldTracks { get; set; }
        public ITrack Track { get; set; }
        public Action<List<ITrack>> OnTracksReady { get;  set; }
        public Action<ITrack> TrackEntered { get; set; }
        public Action<ITrack> TrackLeaving { get; set; }

        public Decoder()
        {
            _airspace = new Airspace(90000, 10000, 90000, 10000, 20000, 500);
            Writer = new LogWriter();
            CollisionDetector = new CollisionDetector();
            Tracks = new List<ITrack>();
            Track = new Track();

            TransponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            TransponderReceiver.TransponderDataReady += DecodeTransData;
        }
        
        public void DecodeTransData(object sender, RawTransponderDataEventArgs e)
        {
            OldTracks = new List<ITrack>(Tracks); 
            Tracks.Clear();

            foreach (var transData in e.TransponderData)
            {
                string[] trackData = transData.Split(';');

                Track.Tag = trackData[0];
                Track.X = int.Parse(trackData[1]);
                Track.Y = int.Parse(trackData[2]);
                Track.Altitude = int.Parse(trackData[3]);
                Track.TimeStamp = Convert.ToDateTime(MakeDateTimeString(trackData[4]));
                Track.Velocity = 0;
                Track.Course = 0;

                
                if (IsInAirspace(Track))
                {
                    // If same track is registered from next TransponderReceiver data, Course and Velocity are calculated
                    if (OldTracks.Any(x => x.Tag == Track.Tag))
                    {
                        ITrack oldTrack = OldTracks.FirstOrDefault(x => x.Tag == Track.Tag);
                        Track.CalculateCourse(oldTrack, Track);
                        Track.CalculateVelocity(oldTrack, Track);
                    }

                    ITrack insertTrack = new Track()
                    {
                        Altitude = Track.Altitude, Course = Track.Course, Tag = Track.Tag,
                        TimeStamp = Track.TimeStamp, Velocity = Track.Velocity, X = Track.X, Y = Track.Y
                    };

                    // Insert track in newest list of tracks
                    Tracks.Add(insertTrack);
                }
            }
            foreach (var track in Tracks)
            {
                if (!OldTracks.Any(x => x.Tag == track.Tag))
                {
                    TrackEntered?.Invoke(track);
                    Writer.Write("Track entered airspace! Time: " + DateTime.Now.ToShortTimeString() + " Tag: " + track.Tag);
                }
            }

            foreach (var track in OldTracks)
            {
                if (!Tracks.Any(x => x.Tag == track.Tag))
                {
                    TrackLeaving?.Invoke(track);
                    Writer.Write("Track leaving airspace! Time: " + DateTime.Now.ToShortTimeString() + " Tag: " + track.Tag);
                }
            }
            //PrintToConsole(Tracks);
            OnTracksReady?.Invoke(Tracks);

            // Check if any two tracks are colliding form list of tracks
            CollisionDetector.DetectCollision(Tracks);
        }

        private string MakeDateTimeString(string dateTime)
        {
            var day = "" + dateTime[6] + dateTime[7];
            var month = "" + dateTime[4] + dateTime[5];
            var year = "" + dateTime[0] + dateTime[1] + dateTime[2] + dateTime[3];
            var hour = "" + dateTime[8] + dateTime[9];
            var minute = "" + dateTime[10] + dateTime[11];
            var second = "" + dateTime[12] + dateTime[13] + "." + dateTime[14] + dateTime[15] + dateTime[16];

            return day + "/" + month + "/" + year + " " + hour + ":" + minute + ":" + second;
        }

        private bool IsInAirspace(ITrack track)
        {
            if (track.X >= _airspace.XLower && track.X <= _airspace.XUpper &&
                track.Y >= _airspace.YLower && track.Y <= _airspace.YUpper &&
                track.Altitude >= _airspace.AltitudeLower && track.Altitude <= _airspace.AltitudeUpper)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void PrintToConsole(List<ITrack> tracks)
        {
            foreach (var track in tracks)
            {
                Writer.Write("Tag: " + track.Tag + " [x, y]: [" + track.X + ", " + 
                             track.Y + "] altitude: " + track.Altitude + " velocity: " + track.Velocity +
                             " course: " + track.Course);
            }
        }

    }
}
