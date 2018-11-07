using System;
using System.Collections.Generic;
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

        public void DetectCollision(List<ITrack> tracks)
        {
            int counter;
            foreach (var track in tracks)
            {
                for (counter = tracks.IndexOf(track) + 1; counter < tracks.Count; counter++)
                {
                    if (CalculateDistance(track.X, track.Y, tracks[counter].X, tracks[counter].Y) < 5000 &&
                        (track.Altitude - tracks[counter].Altitude) < 300)
                    {
                        ConsoleWriter.Write(CreateConsoleText(track.Tag, tracks[counter].Tag, DateTime.Now));
                        LogWriter.Write(CreateConsoleText(track.Tag, tracks[counter].Tag, DateTime.Now));

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
            return time.ToShortTimeString() + ": Collision between " + tag1 + " and " + tag2;
        }
    }
}
