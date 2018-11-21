using System.Collections.Generic;
using AirTrafficMonitor.Implementation;
using AirTrafficMonitor.Interfaces;
using AirTrafficMonitor.Test.Unit;
using NSubstitute;
using NUnit.Framework;

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
        [TestCaseSource(typeof(MockLogWriter_Called_Cases))]
        public void DetectCollision_PossibleCollisionDetected_LogWriterIsCalled(int altitude1, int x1, int y1, int altitude2, int x2, int y2)
        {
            ITrack testTrack1 = new Track() { Altitude = altitude1, X = x1, Y = y2 };
            ITrack testTrack2 = new Track() { Altitude = altitude2, X = x2, Y = y2 };

            List<ITrack> tracks = new List<ITrack>() {testTrack1, testTrack2};

            _uut.DetectCollision(tracks);

            _logWriter.Received().Write(Arg.Any<string>());
        }

        // MOCK: LogWriter is NOT called
        [TestCaseSource(typeof(MockLogWriter_NotCalled_Cases))]
        public void DetectCollision_PossibleCollisionDetected_LogWriterIsNOTCalled(int altitude1, int x1, int y1, int altitude2, int x2, int y2)
        {
            ITrack testTrack1 = new Track() { Altitude = altitude1, X = x1, Y = y2 };
            ITrack testTrack2 = new Track() { Altitude = altitude2, X = x2, Y = y2 };

            List<ITrack> tracks = new List<ITrack>() { testTrack1, testTrack2 };

            _uut.DetectCollision(tracks);

            _logWriter.DidNotReceive().Write(Arg.Any<string>());
        }

        // MOCK: ConsoleWriter is called
        [TestCaseSource(typeof(MockConsoleWriter_Called_Cases))]
        public void DetectCollision_PossibleCollisionDetected_ConsoleWriterIsCalled(int altitude1, int x1, int y1, int altitude2, int x2, int y2)
        {
            ITrack testTrack1 = new Track() { Altitude = altitude1, X = x1, Y = y1 };
            ITrack testTrack2 = new Track() { Altitude = altitude2, X = x2, Y = y2 };

            List<ITrack> tracks = new List<ITrack>() { testTrack1, testTrack2 };

            _uut.DetectCollision(tracks);

            _consoleWriter.Received().Write(Arg.Any<string>());
        }

        // MOCK: ConsoleWriter is NOT called
        [TestCaseSource(typeof(MockConsoleWriter_NotCalled_Cases))]
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
