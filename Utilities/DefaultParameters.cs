using System;
namespace Utilities
{
    public static class DefaultParameters
    {
        public const double StartFreq = 90;

        public static readonly double[] k =
        {
            0.01,
            0.03,
            0.12,
            0.2,
            0.3,
            0.26,
            0.07
        };

        public static readonly double[] deltaA =
        {
            25,
            18,
            14,
            9,
            6,
            5,
            4
        };

        public static readonly double[] HighFreqsExcel =
        {
            181.0,
            356.0,
            711.0,
            1400.0,
            2800.0,
            5599.0,
            11203.0
        };

        public static readonly double[] HighFreqs =
        {
            175.0,
            355.0,
            710.0,
            1400.0,
            2800.0,
            5600.0,
            11200.0
        };

        public static readonly double[] RootFreqs =
        {
            125,
            250,
            500,
            1000,
            2000,
            4000,
            8000
        };

        public static readonly double[] AverageSpeechLevels =
        {
            53,
            66,
            66,
            61,
            56,
            53,
            49
        };

        public static readonly double[] WhiteNoiseLevels =
        {
            61.6,
            61.6,
            61.6,
            61.6,
            61.6,
            61.6,
            61.6
        };

        public static readonly double[] PinkNoiseLevels =
        {
            49.1,
            52.1,
            55.1,
            58.1,
            61.1,
            64.1,
            67.1
        };

        public static readonly double[] BrownNoiseLevels =
        {
            40.4,
            60.4,
            64.4,
            64.4,
            62.4,
            60.4,
            57.4
        };

        public const double NormalizeLevel = 70.0;
    }
}
