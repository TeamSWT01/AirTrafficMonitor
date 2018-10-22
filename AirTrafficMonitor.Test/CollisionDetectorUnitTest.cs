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
        private CollisionDetector _uut;
        private IWriter _logWriter;

        [SetUp]
        public void SetUp()
        {
            _logWriter = Substitute.For<IWriter>();

            _uut = new CollisionDetector()
            {
                LogWriter = _logWriter
            };
        }


    }
}
