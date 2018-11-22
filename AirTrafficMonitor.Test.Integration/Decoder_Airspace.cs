using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirTrafficMonitor.Dto;
using AirTrafficMonitor.Implementation;
using NUnit.Framework;
using NUnit.Framework.Internal;


namespace AirTrafficMonitor.Test.Integration
{
    [TestFixture]
    class Decoder_Airspace
    {
        private Decoder decoder;

        [SetUp]
        public void SetUp()
        {
            decoder = new Decoder();
        }

        [Test]
        public void Decoder_AirspaceInitiated()
        {
            Airspace airspace = new Airspace(90000, 10000, 90000, 10000, 20000, 500);
            Assert.That(decoder._airspace.AltitudeLower, Is.EqualTo(airspace.AltitudeLower));
            Assert.That(decoder._airspace.AltitudeUpper, Is.EqualTo(airspace.AltitudeUpper));
            Assert.That(decoder._airspace.XUpper, Is.EqualTo(airspace.XUpper));
            Assert.That(decoder._airspace.XLower, Is.EqualTo(airspace.XLower));
            Assert.That(decoder._airspace.YUpper, Is.EqualTo(airspace.YUpper));
            Assert.That(decoder._airspace.YLower, Is.EqualTo(airspace.YLower));
        }
    }
}
