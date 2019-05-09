using System;
using Utilities;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = DataProcessing.GetSpectrumFromDataFile("/Users/paul/Desktop/2.txt");

            foreach(var pair in result)
                Console.WriteLine(pair.Key + " " + pair.Value);

            var octaveBands = OctaveBandsProcessing.GetOctaveBandsData(result, 
                                                                       DefaultConfig.StartFreq, 
                                                                       DefaultConfig.HighFreqs, 
                                                                       DefaultConfig.RootFreqs,
                                                                       DefaultConfig.NormalizeLevel);
        }
    }
}
