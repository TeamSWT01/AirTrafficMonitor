using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitor.Dto
{
    public class Airspace
    {
        public int XUpper { get; set; }
        public int XLower { get; set; }
        public int YUpper { get; set; }
        public int YLower { get; set; }
        public int AltitudeUpper { get; set; }
        public int AltitudeLower { get; set; }

        public Airspace(int xUpper, int xLower, int yUpper, int yLower, int altitudeUpper, int altitudeLower)
        {
            XUpper = xUpper;
            XLower = xLower;
            YUpper = yUpper;
            YLower = yLower;
            AltitudeUpper = altitudeUpper;
            AltitudeLower = altitudeLower;
        }
    }
}
