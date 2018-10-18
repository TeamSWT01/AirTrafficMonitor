using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficMonitor.Interfaces
{
    public interface IDecoder
    {
        List<ITrack> Tracks { get; set; }

        void DecodeTransData(object sender, RawTransponderDataEventArgs e);
    }
}
