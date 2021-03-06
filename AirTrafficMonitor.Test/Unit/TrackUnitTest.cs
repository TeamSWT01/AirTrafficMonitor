﻿using System;
using AirTrafficMonitor.Implementation;
using AirTrafficMonitor.Test.Unit;
using AirTrafficMonitor.Interfaces;
using NUnit.Framework;

namespace AirTrafficMonitor.Test
{
    [TestFixture]
    public class TrackUnitTest
    {
        private Track _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new Track();
        }


        // ************************** Property: Course **************************
        [TestCaseSource(typeof(Course_Calculate_Valid_Invalid_Coordinates_ResultCorrect_Cases))]
        public void CourseCalculate_ValidAndInvalid_Coordinates_ResultIsCorrect(int x1, int y1, int x2, int y2, int result)
        {
            ITrack tesTrack1 = new Track() { X = x1, Y = y1, Altitude = 500};
            ITrack tesTrack2 = new Track() { X = x2, Y = y2, Altitude = 500};

            if (IsInAirspace(tesTrack1) && IsInAirspace(tesTrack2))
            {
                _uut.CalculateCourse(tesTrack1, tesTrack2);
            }

            Assert.That(_uut.Course, Is.EqualTo(result));
        }


        // ************************** Property: Velocity **************************
        [Test]
        public void CalculateVelocity_SimpleTestNoIsInAirspaceValidation_ResultIsCorrect()
        {
            ITrack tesTrack1 = new Track() { X = 2, Y = 2, TimeStamp = DateTime.Now };
            ITrack tesTrack2 = new Track() { X = 1, Y = 1, TimeStamp = DateTime.Now.AddSeconds(2) };

            _uut.CalculateVelocity(tesTrack1, tesTrack2);

            Assert.That(Math.Round(_uut.Velocity, 1), Is.EqualTo(0.5));
        }


        // ************************** Property: X **************************
        [TestCaseSource(typeof(X_Coordinate_Valid_Invalid_ResultCorrect_Cases))]
        public void X_Coordinate_ValidAndInvalid_ResultIsCorrect(int x1, int result)
        {
            ITrack tesTrack1 = new Track() { X = x1, Y = 10000, Altitude = 500};

            if (IsInAirspace(tesTrack1))
            {
                _uut.X = tesTrack1.X;
            }

            Assert.That(_uut.X, Is.EqualTo(result));
        }


        // ************************** Property: Y **************************
        [TestCaseSource(typeof(Y_Coordinate_Valid_Invalid_ResultCorrect_Cases))]
        public void Y_Coordinate_ValidAndInvalid_ResultIsCorrect(int y1, int result)
        {
            ITrack tesTrack1 = new Track() { X = 10000, Y = y1, Altitude = 500};

            if (IsInAirspace(tesTrack1))
            {
                _uut.Y = tesTrack1.Y;
            }

            Assert.That(_uut.Y, Is.EqualTo(result));
        }


        // ************************** Property: Altitude **************************
        [TestCaseSource(typeof(Altitude_Coordinate_Valid_Invalid_ResultCorrect_Cases))]
        public void Altitude_ValidAndInvalidCoordinates_ResultIsCorrect(int altitude, int result)
        {
            ITrack tesTrack1 = new Track() { X = 10000, Y = 10000, Altitude = altitude};

            if (IsInAirspace(tesTrack1))
            {
                _uut.Altitude = tesTrack1.Altitude;
            }

            Assert.That(_uut.Altitude, Is.EqualTo(result));
        }


        // ************************** Property: TimeStamp **************************
        [Test]
        public void DateTime_SetDateTimeToNow_ResultIsCorrect()
        {
            ITrack tesTrack1 = new Track() { TimeStamp = DateTime.Now};
            
            _uut.TimeStamp = tesTrack1.TimeStamp;

            Assert.That(RoundUp(_uut.TimeStamp, TimeSpan.FromMinutes(1)), Is.EqualTo(RoundUp(DateTime.Now, TimeSpan.FromMinutes(1))));
        }


        // Helper function to validate if a track is within the defined airspace
        private bool IsInAirspace(ITrack track)
        {
            if (track.X >= 10000 && track.X <= 90000 && track.Y >= 10000 && track.Y <= 90000 &&
                track.Altitude >= 500 && track.Altitude <= 20000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Helper function to round up DateTime to nearest minute
        DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }
    }
}
