using System;
namespace Models
{
    public class OctaveBand
    {
        public double LowFreq { get; set; }
        public double HighFreq { get; set; }
        public double RootFreq { get; set; }

        public double SumLevel { get; set; }
        public double IntegralLevel { get; set; }
        public double NormalizedLevel { get; set; }
    }
}
