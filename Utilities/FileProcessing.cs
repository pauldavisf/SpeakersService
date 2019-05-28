using System;
using System.Collections.Generic;

namespace Utilities
{
    public static class FileProcessing
    {
        public static void PutFreqStatToFile(string filename, string datFile)
        {
            string command = string.Format("\"C:\\Program Files (x86)\\sox-14-4-2\\sox\" \"{0}\" -n stat -freq > \"{1}\" 2>&1", filename, datFile);
            command.Bash();
        }

        public static Dictionary<string, double[]> GetIntelligibilityForAllNoises(double[] speech, Dictionary<string, double[]> noises)
        {
            var result = new Dictionary<string, double[]>();

            foreach (var pair in noises)
            {
                result[pair.Key] = GetIntelligibility(speech, pair.Value);
            }

            return result;
        }

        public static double[] GetSpeechLevelsFromFile(string filename)
        {
            string fileDirectory = System.IO.Path.GetDirectoryName(filename);
            var freqsFile = fileDirectory + "\\freqs.dat";

            PutFreqStatToFile(filename, freqsFile);

            // берем спектр из файла с частотами
            var spectrum = DataProcessing.GetSpectrumFromDataFile(freqsFile);

            // рассчитываем уровни по октавным полосам
            var octaveBands = OctaveBandsProcessing.GetOctaveBandsData(spectrum,
                                                                       DefaultParameters.StartFreq,
                                                                       DefaultParameters.HighFreqs,
                                                                       DefaultParameters.RootFreqs,
                                                                       DefaultParameters.NormalizeLevel);

            var speech = OctaveBandsProcessing.GetNormalizedLevels(octaveBands);

            return speech;
        }

        public static double[] GetIntelligibility(double[] speech, double[] noise)
        {
            // считаем разборчивость в каждой полосе

            var W = Intelligibility.GetWRange(speech,
                                      noise,
                                      DefaultParameters.k,
                                      DefaultParameters.deltaA,
                                      -30,
                                      30);

            return W;
        }
    }
}
