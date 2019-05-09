using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Utilities
{
    public static class OctaveBandsProcessing
    {
        public static double GetSumLevel(IEnumerable<double> levels)
        {
            return levels.Sum();
        }

        public static double GetIntegralLevel(double sum)
        {
            return 10 * Math.Log10(sum);
        }

        public static double GetNormalizedLevel(double integralLevel, double sumOfSumLevels, double normalizeConst)
        {
            var logOfSum = 10 * Math.Log10(sumOfSumLevels); // N3
            var diff = logOfSum - normalizeConst; // E4
            return integralLevel - diff;
        }

        public static OctaveBands GetOctaveBandsData(Dictionary<double, double> data, 
                                                     double startFreq, 
                                                     double[] highFreqs, 
                                                     double[] rootFreqs, 
                                                     double normalizeConst)
        {
            var result = new OctaveBands(highFreqs.Length);

            double currentStartFreq = startFreq;
            var sumOfSumLevels = 0.0;
            for (int i = 0; i < highFreqs.Length; i++)
            {
                var octaveBand = new OctaveBand();
                var highFreq = highFreqs[i];
                octaveBand.LowFreq = currentStartFreq;
                octaveBand.HighFreq = highFreqs[i];
                octaveBand.RootFreq = rootFreqs[i];

                var levels = data.Where(x => x.Key > currentStartFreq && x.Key < highFreq).Select(x => x.Value);

                octaveBand.SumLevel = GetSumLevel(levels);
                sumOfSumLevels += octaveBand.SumLevel;
                octaveBand.IntegralLevel = GetIntegralLevel(octaveBand.SumLevel);

                currentStartFreq = highFreq;

                result[i] = octaveBand;
            }

            foreach(var band in result.Bands)
            {
                band.NormalizedLevel = GetNormalizedLevel(band.IntegralLevel, sumOfSumLevels, normalizeConst);
            }

            return result;
        }
    }
}
