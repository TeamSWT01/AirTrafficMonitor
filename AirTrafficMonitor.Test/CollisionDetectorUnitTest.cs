using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitor.Implementation;
using AirTrafficMonitor.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AirTrafficMonitor.Test
{
    [TestFixture()]
    public class CollisionDetectorUnitTest
    {
        // Declaring Unit Under Test and objects for all dependencies
        private CollisionDetector _uut;
        private IWriter _logWriter;
        private IWriter _consoleWriter;

        [SetUp]
        public void SetUp()
        {
            // Instantiating dependencies to fakes
            _logWriter = Substitute.For<IWriter>();
            _consoleWriter = Substitute.For<IWriter>();

            // Injecting fakes into class CollisionDetector (UUT)
            _uut = new CollisionDetector()
            {
                LogWriter = _logWriter,
                ConsoleWriter = _consoleWriter
            };
        }

        // MOCK: LogWriter is called
        [TestCase(1000, 14999, 15001, 701, 14000, 14000)]
        [TestCase(20000, 90000, 90000, 19701, 85001, 85001)]
        public void DetectCollision_PossibleCollisionDetected_LogWriterIsCalled(int altitude1, int x1, int y1, int altitude2, int x2, int y2)
        {
            ITrack testTrack1 = new Track() { Altitude = altitude1, X = x1, Y = y2 };
            ITrack testTrack2 = new Track() { Altitude = altitude2, X = x2, Y = y2 };

            List<ITrack> tracks = new List<ITrack>() {testTrack1, testTrack2};

            _uut.DetectCollision(tracks);

            _logWriter.Received().Write(Arg.Any<string>());
        }

        // MOCK: LogWriter is NOT called
        [TestCase(1000, 15000, 15000, 701, 10000, 10000)] // Altitude less than 300 meters, but airplane coordinates more than 4999 meters from each other
        [TestCase(1000, 14999, 15000, 700, 10000, 10000)] // X coordinate less than 5000 meters from each other, but altitude more than 299 meters
        [TestCase(1000, 15000, 14999, 700, 10000, 10000)] // Y coordinate less than 5000 meters from each other, but altitude more than 299 meters
        public void DetectCollision_PossibleCollisionDetected_LogWriterIsNOTCalled(int altitude1, int x1, int y1, int altitude2, int x2, int y2)
        {
            ITrack testTrack1 = new Track() { Altitude = altitude1, X = x1, Y = y2 };
            ITrack testTrack2 = new Track() { Altitude = altitude2, X = x2, Y = y2 };

            List<ITrack> tracks = new List<ITrack>() { testTrack1, testTrack2 };

            _uut.DetectCollision(tracks);

            _logWriter.DidNotReceive().Write(Arg.Any<string>());
        }

        // MOCK: ConsoleWriter is called
        [TestCase(1000, 14999, 15001, 701, 14000, 14000)]
        //[TestCase(20000, 90000, 90000, 19701, 85001, 85001)]
        public void DetectCollision_PossibleCollisionDetected_ConsoleWriterIsCalled(int altitude1, int x1, int y1, int altitude2, int x2, int y2)
        {
            ITrack testTrack1 = new Track() { Altitude = altitude1, X = x1, Y = y1 };
            ITrack testTrack2 = new Track() { Altitude = altitude2, X = x2, Y = y2 };

            List<ITrack> tracks = new List<ITrack>() { testTrack1, testTrack2 };

            _uut.DetectCollision(tracks);

            _consoleWriter.Received().Write(Arg.Any<string>());
        }

        // MOCK: ConsoleWriter is NOT called
        [TestCase(1000, 15000, 15000, 701, 10000, 10000)] // Altitude less than 300 meters, but airplane coordinates more than 4999 meters from each other
        [TestCase(1000, 14999, 15000, 700, 10000, 10000)] // X coordinate less than 5000 meters from each other, but altitude more than 299 meters
        [TestCase(1000, 15000, 14999, 700, 10000, 10000)] // Y coordinate less than 5000 meters from each other, but altitude more than 299 meters
        public void DetectCollision_PossibleCollisionDetected_ConsoleWriterIsNOTCalled(int altitude1, int x1, int y1, int altitude2, int x2, int y2)
        {
            ITrack testTrack1 = new Track() { Altitude = altitude1, X = x1, Y = y1 };
            ITrack testTrack2 = new Track() { Altitude = altitude2, X = x2, Y = y2 };

            List<ITrack> tracks = new List<ITrack>() { testTrack1, testTrack2 };

            _uut.DetectCollision(tracks);

            _consoleWriter.DidNotReceive().Write(Arg.Any<string>());
        }



    }
}
