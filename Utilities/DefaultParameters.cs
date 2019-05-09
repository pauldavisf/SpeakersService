using System;
namespace Utilities
{
    public static class DefaultParameters
    {
        public const double StartFreq = 87.0;

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

        public static readonly double[] HighFreqs =
        {
            181.0,
            356.0,
            711.0,
            1400.0,
            2800.0,
            5599.0,
            11203.0
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

        public const double NormalizeLevel = 70.0;
    }
}
