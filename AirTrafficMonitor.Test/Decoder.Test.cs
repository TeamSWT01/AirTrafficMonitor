using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirTrafficMonitor.Implementation;
using AirTrafficMonitor.Interfaces;
using Castle.Core.Internal;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TransponderReceiver;

namespace AirTrafficMonitor.Test
{
    [TestFixture]
    public class Class1
    {
        private Decoder _uut;
        private ICollisionDetector _collisionDetector;
        private ITrack _track;
        private IWriter _writer;
        private ITransponderReceiver _transponderReceiver;

        [SetUp]
        public void Setup()
        {
            _collisionDetector = Substitute.For<ICollisionDetector>();
            _track = Substitute.For<ITrack>();
            _writer = Substitute.For<IWriter>();
            _transponderReceiver = Substitute.For<ITransponderReceiver>();

            _uut = new Decoder();
            _uut.Writer = _writer;
            _uut.CollisionDetector = _collisionDetector;
            _uut.Track = _track;
            _uut.TransponderReceiver = _transponderReceiver;
        }

        // Is in Airspace?
        [TestCase("ABC123", "9999", "9999", "1000", "20180427120717123", false)]
        [TestCase("ABC123", "10000", "10000", "1000", "20180427120717123", true)]
        [TestCase("ABC123", "1000", "10000", "1000", "20180427120717123", false)]
        [TestCase("ABC123", "90000", "90000", "1000", "20180427120717123", true)]
        [TestCase("ABC123", "90001", "90001", "1000", "20180427120717123", false)]
        [TestCase("ABC123", "90000", "90001", "1000", "20180427120717123", false)]
        public void DecodeTransData_TrackIsInAirspace_ResultAsExpected(string tag, string x, string y, string altitude,
            string dateTime, bool result)
        {
            List<string> list=new List<string>(){tag + ';' + x + ';' + y + ';' + altitude + ';' + dateTime};
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list));

            ITrack someTrack = new Track()
            {
                Altitude = int.Parse(altitude), Tag = tag, TimeStamp = Convert.ToDateTime(MakeDateTimeString(dateTime)),
                X = int.Parse(x), Y = int.Parse(y)
            };

            Assert.That(_uut.Tracks.IsNullOrEmpty(), Is.EqualTo(!result));
        }

        [TestCase("ABC123", "10000", "10000", "1000", "20000101101010999")]
        public void DecodeTransData_IsInAirspace_CalculateVelocityIsCalled(string tag, string x, string y, string altitude,
            string dateTime)
        {
            List<string> list = new List<string>() { tag + ';' + x + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list));

            List<string> list2 = new List<string>() { tag + ';' + "12000" + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list2));

            _track.Received().CalculateVelocity(Arg.Any<ITrack>(), Arg.Any<ITrack>());
        }

        [TestCase("ABC123", "10000", "10000", "1000", "20000101101010999")]
        public void DecodeTransData_IsInAirspace_CalculateCourseIsCalled(string tag, string x, string y, string altitude,
            string dateTime)
        {
            List<string> list = new List<string>() { tag + ';' + x + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list));

            List<string> list2 = new List<string>() { tag + ';' + "12000" + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list2));

            _track.Received().CalculateCourse(Arg.Any<ITrack>(), Arg.Any<ITrack>());
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
    }
}
