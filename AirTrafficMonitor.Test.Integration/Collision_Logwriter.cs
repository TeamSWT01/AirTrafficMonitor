using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Implementation;
using AirTrafficMonitor.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;


namespace AirTrafficMonitor.Test.Integration

{

    [TestFixture()]

    public class Collision_Logwriter

    {
        private CollisionDetector collisionDetector;

        [SetUp]
        public void SetUp()
        {
            collisionDetector = new CollisionDetector();
        }
        

        [TestCase( 1000, 14999, 15001, 701, 14000, 14000 )]
        [TestCase(20000, 90000, 90000, 19701, 85001, 85001)]
        public void DetectCollision_SeparationEvent_WriteToLog(int altitude1, int x1, int y1, int altitude2, int x2, int y2)
        {
            ITrack testTrack1 = new Track() { Tag = "XY123456", Altitude = altitude1, X = x1, Y = y2, TimeStamp = DateTime.Now };
            ITrack testTrack2 = new Track() { Tag = "II000000", Altitude = altitude2, X = x2, Y = y2, TimeStamp = DateTime.Now };

            List<ITrack> tracks = new List<ITrack>() { testTrack1, testTrack2 };

            collisionDetector.DetectCollision(tracks);

            var lastLine = File.ReadLines(@"Track.txt").Last();
            Assert.That(lastLine, Is.EqualTo(DateTime.Now.ToShortTimeString() + ": Collision between " + testTrack1.Tag + " and " + testTrack2.Tag));
        }
    }
}