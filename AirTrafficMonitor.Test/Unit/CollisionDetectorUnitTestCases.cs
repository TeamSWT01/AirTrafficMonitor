using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitor.Test.Unit
{    
    public class MockLogWriter_Called_Cases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new[] { 1000, 14999, 15001, 701, 14000, 14000 };
            yield return new[] { 20000, 90000, 90000, 19701, 85001, 85001 };
        }
    }

    public class MockLogWriter_NotCalled_Cases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new[] { 1000, 15000, 15000, 701, 10000, 10000 };  // Altitude less than 300 meters, but airplane coordinates more than 4999 meters from each other
            yield return new[] { 1000, 14999, 15000, 700, 10000, 10000 };  // X coordinate less than 5000 meters from each other, but altitude more than 299 meters        
            yield return new[] { 1000, 15000, 14999, 700, 10000, 10000 };  // Y coordinate less than 5000 meters from each other, but altitude more than 299 meters        
        }
    }


    public class MockConsoleWriter_Called_Cases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new[] { 1000, 14999, 15001, 701, 14000, 14000 };
            yield return new[] { 20000, 90000, 90000, 19701, 85001, 85001 };
        }
    }

    public class MockConsoleWriter_NotCalled_Cases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new[] { 1000, 15000, 15000, 701, 10000, 10000 }; // Altitude less than 300 meters, but airplane coordinates more than 4999 meters from each other
            yield return new[] { 1000, 14999, 15000, 700, 10000, 10000 }; // X coordinate less than 5000 meters from each other, but altitude more than 299 meters
            yield return new[] { 1000, 15000, 14999, 700, 10000, 10000 }; // Y coordinate less than 5000 meters from each other, but altitude more than 299 meters
        }
    }
}
