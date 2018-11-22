using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitor.Test.Integration
{
    public class SeperationEvent_WriteToLog : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new[] { 1000, 14999, 15001, 701, 14000, 14000 };
            yield return new[] { 20000, 90000, 90000, 19701, 85001, 85001 };
        }
    }
}
