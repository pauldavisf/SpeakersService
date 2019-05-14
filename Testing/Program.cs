using System;
using Utilities;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = "/Users/paul/Desktop/Files/ж1_1.wav";
            var dataFile = "/Users/paul/Desktop/freqs.txt";
            FileProcessing.PutFreqStatToFile(fileName, dataFile);

            var result = DataProcessing.GetSpectrumFromDataFile(dataFile);

            foreach(var pair in result)
                Console.WriteLine(pair.Key + " " + pair.Value);

            var octaveBands = OctaveBandsProcessing.GetOctaveBandsData(result, 
                                                                       DefaultParameters.StartFreq, 
                                                                       DefaultParameters.HighFreqs, 
                                                                       DefaultParameters.RootFreqs,
                                                                       DefaultParameters.NormalizeLevel);

            var speech = new double[octaveBands.Count];

            Console.WriteLine("");
            Console.WriteLine("Нормализованные уровни для речи:");
            for (int i = 0; i < speech.Length; i++)
            {
                Console.WriteLine(octaveBands[i].NormalizedLevel);
                speech[i] = octaveBands[i].NormalizedLevel;
            }

            Console.WriteLine("");
            Console.WriteLine("Разборчивость:");

            var testW = Intelligibility.W(speech, 
                                          DefaultParameters.WhiteNoiseLevels, 
                                          DefaultParameters.k, 
                                          DefaultParameters.deltaA, 
                                          -30, 
                                          30);

            foreach (var w in testW)
                Console.WriteLine(w);


            Console.WriteLine("");
            Console.WriteLine("Разборчивость напрямую:");

            var directW = FileProcessing.GetIntelligibility(fileName);

            foreach (var w in directW)
                Console.WriteLine(w);
        }
    }
}
