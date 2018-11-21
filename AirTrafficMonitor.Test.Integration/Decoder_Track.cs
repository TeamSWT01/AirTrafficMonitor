using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirTrafficMonitor.Implementation;
using AirTrafficMonitor.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiver;

namespace AirTrafficMonitor.Test.Integration
{
    [TestFixture]
    class Decoder_Track
    {
        private Decoder decoder;

        [SetUp]
        public void SetUp()
        {
            decoder = new Decoder();
        }

        [TestCase("ABC123", "10000", "10000", "1000", "20000101101010999")]
        public void DecodeTransData_IsInAirspace_CalculateVelocityIsCalled(string tag, string x, string y,
            string altitude, string dateTime)
        {
            List<string> list = new List<string>() { tag + ';' + x + ';' + y + ';' + altitude + ';' + dateTime };
            decoder.DecodeTransData(new object(), new RawTransponderDataEventArgs(list));

            List<string> list2 = new List<string>() { tag + ';' + "12000" + ';' + y + ';' + altitude + ';' + "20000101101020999" };
            decoder.DecodeTransData(new object(), new RawTransponderDataEventArgs(list2));

            //decoder.Track = new Track(){500, 270, "XX1234", DateTime.Now, 50, 10000, 10000};

            Assert.That(Math.Round(decoder.Track.Velocity, 1), Is.EqualTo(200));
        }

        [TestCase("ABC123", "10000", "10000", "1000", "20000101101010999")]
        public void DecodeTransData_IsInAirspace_CalculateCourseIsCalled(string tag, string x, string y,
            string altitude, string dateTime)
        {
            List<string> list = new List<string>() { tag + ';' + x + ';' + y + ';' + altitude + ';' + dateTime };
            decoder.DecodeTransData(new object(), new RawTransponderDataEventArgs(list));

            List<string> list2 = new List<string>() { tag + ';' + "12000" + ';' + y + ';' + altitude + ';' + "20000101101020999" };
            decoder.DecodeTransData(new object(), new RawTransponderDataEventArgs(list2));

            Assert.That(decoder.Track.Course, Is.EqualTo(270));
        }
    }
}
