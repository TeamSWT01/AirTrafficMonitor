using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using TransponderReceiver;
using Decoder = AirTrafficMonitor.Implementation.Decoder;

namespace AirTrafficMonitor.Test.Integration
{
    [TestFixture()]
    public class Decoder_Logwriter
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

            var lastLine = File.ReadLines(@"Track.txt").Last();
            Assert.That(lastLine, Is.EqualTo("Track entered airspace! Time: " + DateTime.Now.ToShortTimeString() + " Tag: " + tag));
        }
    }
}
