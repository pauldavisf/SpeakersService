using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Utilities
{
    public static class DataProcessing
    {
        private static KeyValuePair<double, double> ParseLine(string line)
        {
            var splitted = line.Split(' ');

            var style = NumberStyles.AllowDecimalPoint;
            var culture = CultureInfo.InvariantCulture;
            if (double.TryParse(splitted[0], style, culture, out var freq) &&
                double.TryParse(splitted[2], style, culture, out var amp))
            {
                return new KeyValuePair<double, double>(freq, amp);
            }
            else
            {
                throw new ArgumentException(string.Format("Wrong line format: {0}", line));
            }
        }

        public static Dictionary<double, double> GetSpectrumFromDataFile(string filename)
        {
            var result = new Dictionary<double, double>();

            var summOfAmps = new Dictionary<double, double>();
            var blocksCount = 0;

            var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var parsedPair = ParseLine(line);

                    if (summOfAmps.ContainsKey(parsedPair.Key))
                    {
                        summOfAmps[parsedPair.Key] += parsedPair.Value;

                        if (parsedPair.Key == 0.0)
                        {
                            blocksCount++;
                        }
                    }
                    else
                    {
                        summOfAmps[parsedPair.Key] = parsedPair.Value;
                    }
                }
            }

            foreach(var pair in summOfAmps)
            {
                result[pair.Key] = summOfAmps[pair.Key] / blocksCount;
            }

            return result;
        }
    }
}
