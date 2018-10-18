using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Interfaces;

namespace AirTrafficMonitor.Implementation
{
    public class CollisionDetector : ICollisionDetector
    {
        public IWriter ConsoleWriter { get; set; }
        public IWriter LogWriter { get; set; }

        public CollisionDetector()
        {
            ConsoleWriter = new ConsoleWriter();
            LogWriter = new LogWriter();
        }

        public void DetectCollision(List<ITrack> Tracks)
        {
            int counter;
            foreach (var track in Tracks)
            {
                for (counter = Tracks.IndexOf(track) + 1; counter < Tracks.Count; counter++)
                {
                    if (CalculateDistance(track.X, track.Y, Tracks[counter].X, Tracks[counter].Y) < 5000 &&
                        (track.Altitude - Tracks[counter].Altitude) < 300)
                    {
                        ConsoleWriter.Write(CreateConsoleText(track.Tag, Tracks[counter].Tag, DateTime.Now));
                        LogWriter.Write(CreateConsoleText(track.Tag, Tracks[counter].Tag, DateTime.Now));

                    }
                }

            }
        }

        private int CalculateDistance(int x1, int y1, int x2, int y2)
        {
            return (int)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)); 
        }

        private string CreateConsoleText(string tag1, string tag2, DateTime time)
        {
            return time.ToLongTimeString() + ": Collision between " + tag1 + " and " + tag2;
        }
    }
}
