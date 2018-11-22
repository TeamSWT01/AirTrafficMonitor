using System;
using System.Collections.Generic;
using AirTrafficMonitor.Implementation;
using AirTrafficMonitor.Interfaces;
using AirTrafficMonitor.Test.Unit;
using Castle.Core.Internal;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitor.Test
{
    [TestFixture]
    public class DecoderUnitTest
    {
        // Declaring Unit Under Test and objects for all dependencies
        private Decoder _uut;
        private ICollisionDetector _collisionDetector;
        private ITrack _track;
        private IWriter _writer;
        private ITransponderReceiver _transponderReceiver;
        private bool OnTracksReady;
        private bool TrackEntered;
        private bool TrackLeaving;

        [SetUp]
        public void Setup()
        {
            // Instantiating dependencies to fakes
            _collisionDetector = Substitute.For<ICollisionDetector>();
            _track = Substitute.For<ITrack>();
            _writer = Substitute.For<IWriter>();
            _transponderReceiver = Substitute.For<ITransponderReceiver>();

            OnTracksReady = false;
            TrackEntered = false;
            TrackLeaving = false;

            // Injecting fakes into class Decoder (UUT)
            _uut = new Decoder
            {
                Writer = _writer,
                CollisionDetector = _collisionDetector,
                Track = _track,
                TransponderReceiver = _transponderReceiver
            };

            _uut.OnTracksReady += (List<ITrack> tracks) => { OnTracksReady = true; };
            _uut.TrackEntered += (ITrack track) => { TrackEntered = true; };
            _uut.TrackLeaving += (ITrack track) => { TrackLeaving = true; };
        }

        [TestCaseSource(typeof(DecodeTransData_TrackIsInAirspace_ResultAsExpected_Cases))]
        public void DecodeTransData_TrackIsInAirspace_ResultAsExpected(string tag, string x, string y, string altitude, string dateTime, bool result)
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


        // MOCK: Testing ITrack - is Track.CalculateVelocity() called with valid input?
       [TestCaseSource(typeof(MockTesting_ITrack_CalculateVelocityIsCalled_ValidInput_Cases) )]
        public void DecodeTransData_IsInAirspace_CalculateVelocityIsCalled(string tag, string x, string y, string altitude, string dateTime)
        {
            List<string> list = new List<string>() { tag + ';' + x + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list));

            List<string> list2 = new List<string>() { tag + ';' + "12000" + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list2));

            _track.Received().CalculateVelocity(Arg.Any<ITrack>(), Arg.Any<ITrack>());
        }

        // MOCK: Testing ITrack - is Track.CalculateVelocity() called with invalid input?
        [TestCaseSource(typeof(MockTesting_ITrack_CalculateVelocityIsCalled_InvalidInput_Cases))]
        public void DecodeTransData_IsNOTInAirspace_CalculateVelocityIsCalled(string tag, string x, string y, string altitude, string dateTime)
        {
            List<string> list = new List<string>() { tag + ';' + x + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list));

            List<string> list2 = new List<string>() { tag + ';' + "12000" + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list2));

            _track.DidNotReceive().CalculateVelocity(Arg.Any<ITrack>(), Arg.Any<ITrack>());
        }


        // MOCK: Testing ITrack - is Track.CalculateCourse() called with valid input?
        [TestCase("ABC123", "10000", "10000", "1000", "20000101101010999")]
        public void DecodeTransData_IsInAirspace_CalculateCourseIsCalled(string tag, string x, string y,
            string altitude, string dateTime)
        {
            List<string> list = new List<string>() { tag + ';' + x + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list));

            List<string> list2 = new List<string>() { tag + ';' + "12000" + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list2));

            _track.Received().CalculateCourse(Arg.Any<ITrack>(), Arg.Any<ITrack>());
        }


        // MOCK: Testing ITrack - is Track.CalculateCourse() called with invalid input?
        [TestCaseSource(typeof(MockTesting_ITrack_CalculateCourseIsCalled_InValidInput_Cases))]
        public void DecodeTransData_IsNOTInAirspace_CalculateCourseIsNOTCalled(string tag, string x, string y, string altitude, string dateTime)
        {
            List<string> list = new List<string>() { tag + ';' + x + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list));

            List<string> list2 = new List<string>() { tag + ';' + "12000" + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list2));

            _track.DidNotReceive().CalculateCourse(Arg.Any<ITrack>(), Arg.Any<ITrack>());
        }


        // MOCK: Testing IWriter - is Write() called?
        [TestCaseSource(typeof(MockTesting_IWriter_Cases))]
        public void DecodeTransData_IsInAirspace_WriteIsCalled(string tag, string x, string y, string altitude, string dateTime)
        {
            List<string> list = new List<string>() { tag + ';' + x + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list));

            _writer.Received().Write(Arg.Any<string>());
        }


        // MOCK: Testing ICollisionDetector - is CollisionDetector.DetectCollision() called?
        [TestCaseSource(typeof(MockTesting_ICollisionDetector_DetectCollisionIsCalled_Cases))]
        public void DecodeTransData_IsInAirspace_DetectCollisionIsCalled(string tag, string x, string y, string altitude, string dateTime)
        {
            List<string> list = new List<string>() { tag + ';' + x + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list));

            _collisionDetector.Received().DetectCollision(Arg.Any<List<ITrack>>());
        }

        [TestCase("ABC123", "10000", "10000", "1000", "20000101101010999")]
        public void DecodeTransData_OnTracksReady_EventIsCalled(string tag, string x, string y, string altitude, string dateTime)
        {
            List<string> list = new List<string>() { tag + ';' + x + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list));

            Assert.That(OnTracksReady, Is.EqualTo(true));
        }

        [TestCase("ABC123", "10000", "10000", "1000", "20000101101010999")]
        public void DecodeTransData_TrackEntered_EventIsCalled(string tag, string x, string y, string altitude, string dateTime)
        {
            List<string> list = new List<string>() { tag + ';' + x + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list));

            Assert.That(TrackEntered, Is.EqualTo(true));
        }

        [TestCase("ABC123", "10000", "10000", "1000", "20000101101010999")]
        public void DecodeTransData_TrackLeaving_EventIsCalled(string tag, string x, string y, string altitude, string dateTime)
        {
            List<string> list = new List<string>() { tag + ';' + x + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list));

            List<string> list2 = new List<string>() { tag + ';' + "90001" + ';' + y + ';' + altitude + ';' + dateTime };
            _uut.DecodeTransData(new object(), new RawTransponderDataEventArgs(list2));

            Assert.That(TrackLeaving, Is.EqualTo(true));
        }


        // Helper function to determine dateTime
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
