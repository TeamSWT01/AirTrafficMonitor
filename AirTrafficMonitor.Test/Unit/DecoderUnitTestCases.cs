using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitor.Test.Unit
{
    public class DecodeTransData_TrackIsInAirspace_ResultAsExpected_Cases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new Object[] { "ABC123", "9999", "9999", "1000", "20180427120717123", false };
            yield return new Object[] { "ABC123", "10000", "10000", "1000", "20180427120717123", true };
            yield return new Object[] { "ABC123", "1000", "10000", "1000", "20180427120717123", false };
            yield return new Object[] { "ABC123", "90000", "90000", "1000", "20180427120717123", true };
            yield return new Object[] { "ABC123", "90001", "90001", "1000", "20180427120717123", false };
            yield return new Object[] { "ABC123", "90000", "90001", "1000", "20180427120717123", false };
        }
    }

    public class MockTesting_ITrack_CalculateVelocityIsCalled_ValidInput_Cases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new[] { "ABC123", "10000", "10000", "1000", "20000101101010999" };            
        }
    }

    public class MockTesting_ITrack_CalculateVelocityIsCalled_InvalidInput_Cases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new[] { "ABC123", "90001", "90001", "1000", "20000101101010999" };            
        }
    }

    public class MockTesting_ITrack_CalculateCourseIsCalled_ValidInput_Cases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new[] { "ABC123", "10000", "10000", "1000", "20000101101010999" };            
        }
    }

    public class MockTesting_ITrack_CalculateCourseIsCalled_InValidInput_Cases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new[] { "ABC123", "9999", "9999", "1000", "20000101101010999" };            
        }
    }


    public class MockTesting_IWriter_Cases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new[] { "ABC123", "10000", "10000", "1000", "20000101101010999" };            
        }
    }

    public class MockTesting_ICollisionDetector_DetectCollisionIsCalled_Cases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new[] { "ABC123", "10000", "10000", "1000", "20000101101010999" };            
        }
    }
   





}
