using System.Collections.Generic;
using AirTrafficMonitor.Implementation;
using AirTrafficMonitor.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitor.Test.Integration
{
    [TestFixture]
    public class Decoder_Collision
    {
        private Decoder decoder;
        private IWriter _writer;

        [SetUp]
        public void SetUp()
        {
            decoder = new Decoder();
            _writer = Substitute.For<IWriter>();
            decoder.CollisionDetector.LogWriter = _writer;
        }

        [TestCase("ABC123", "10000", "10000", "1000", "20180427120717123")]
        public void DecodeTransData_TrackIsInAirspace_ResultAsExpected(string tag, string x, string y, string altitude,
            string dateTime)
        {
            List<string> list = new List<string>() { tag + ';' + x + ';' + y + ';' + altitude + ';' + dateTime,
                                                     "XX1234" + ';' + x + ';' + y + ';' + altitude + ';' + dateTime };

            decoder.DecodeTransData(new object(), new RawTransponderDataEventArgs(list));

            _writer.Received().Write(Arg.Any<string>());
        }
    }
}
