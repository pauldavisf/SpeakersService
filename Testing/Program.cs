using System;
using Utilities;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataFile = "/Users/paul/Desktop/freqs.txt";
            FileProcessing.PutFreqStatToFile("/Users/paul/Desktop/1.wav", dataFile);

            var result = DataProcessing.GetSpectrumFromDataFile(dataFile);

            foreach(var pair in result)
                Console.WriteLine(pair.Key + " " + pair.Value);

            var octaveBands = OctaveBandsProcessing.GetOctaveBandsData(result, 
                                                                       DefaultParameters.StartFreq, 
                                                                       DefaultParameters.HighFreqs, 
                                                                       DefaultParameters.RootFreqs,
                                                                       DefaultParameters.NormalizeLevel);

            var testW = Intelligibility.W(DefaultParameters.AverageSpeechLevels, 
                                          DefaultParameters.WhiteNoiseLevels, 
                                          DefaultParameters.k, 
                                          DefaultParameters.deltaA, 
                                          -30, 
                                          30);

            foreach (var w in testW)
                Console.WriteLine(w);
        }
    }
}
