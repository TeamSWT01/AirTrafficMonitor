using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitor.Test.Unit
{

    public class Course_Calculate_Valid_Invalid_Coordinates_ResultCorrect_Cases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new[] { 20000, 20000, 10000, 10000, 45 };
            yield return new[] { 20000, 20000, 9999, 10000, 0 };
            yield return new[] { 20000, 20000, 10000, 9999, 0 };
            yield return new[] { 90000, 11000, 10000, 10000, 89 };
            yield return new[] { 90001, 20000, 10000, 10000, 0 };
            yield return new[] { 20000, 90001, 10000, 10000, 0 };
        }
    }


    public class X_Coordinate_Valid_Invalid_ResultCorrect_Cases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new[] { 10000, 10000 };
            yield return new[] { 9999, 0 };
            yield return new[] { 90000, 90000 };
            yield return new[] { 90001, 0 };
        }
    }


    public class Y_Coordinate_Valid_Invalid_ResultCorrect_Cases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new[] { 10000, 10000 };
            yield return new[] { 9999, 0 };
            yield return new[] { 90000, 90000 };
            yield return new[] { 90001, 0 };
        }
    }

    public class Altitude_Coordinate_Valid_Invalid_ResultCorrect_Cases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new[] { 500, 500 };
            yield return new[] { 499, 0 };
            yield return new[] { 20000, 20000 };
            yield return new[] { 20001, 0 };
        }
    }
}
