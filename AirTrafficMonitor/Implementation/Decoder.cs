using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Dto;
using AirTrafficMonitor.Interfaces;
using TransponderReceiver;

namespace AirTrafficMonitor.Implementation
{
    public class Decoder : IDecoder
    {
        public ITransponderReceiver TransponderReceiver { get; set; }
        private Airspace airspace;
        public IWriter Writer { get; set; }
        public ICollisionDetector CollisionDetector { get; set; }
        public List<ITrack> Tracks { get; set; }

        public Decoder()
        {
            airspace = new Airspace(90000, 10000, 90000, 10000, 20000, 500);
            Writer = new ConsoleWriter();
            CollisionDetector = new CollisionDetector();
            Tracks = new List<ITrack>();

            TransponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            TransponderReceiver.TransponderDataReady += DecodeTransData;
        }
        
        public void DecodeTransData(object sender, RawTransponderDataEventArgs e)
        {
            Tracks.Clear();

            foreach (var transData in e.TransponderData)
            {
                string[] TrackData = transData.Split(';');
                ITrack newTrack = new Track();

                newTrack.Tag = TrackData[0];
                newTrack.X = int.Parse(TrackData[1]);
                newTrack.Y = int.Parse(TrackData[2]);
                newTrack.Altitude = int.Parse(TrackData[3]);
                newTrack.TimeStamp = Convert.ToDateTime(MakeDateTimeString(TrackData[4]));
                
                if (IsInAirspace(newTrack))
                {
                    Tracks.Add(newTrack);
                }
            }

            PrintToConsole(Tracks);
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
            if (track.X >= airspace.XLower && track.X <= airspace.XUpper &&
                track.Y >= airspace.YLower && track.Y <= airspace.YUpper &&
                track.Altitude >= airspace.AltitudeLower && track.Altitude <= airspace.AltitudeUpper)
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
                Writer.Write("Tag: " + track.Tag + " x-coordinate: " + track.X + " y-coordinate " + 
                             track.Y + " altitude: " + track.Altitude + " timestamp: " + track.TimeStamp);
            }
        }

    }
}
