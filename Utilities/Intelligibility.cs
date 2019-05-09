using System;
using System.Linq;

namespace Utilities
{
    public static class Intelligibility
    {
        public static double[] W(double[] speech, double[] noise, double[] k, double[] deltaA, int from, int to)
        {
            var w = new double[to - from];

            int i = 0;
            for (int q = from; q <= to; q++)
            {
                var rSum = Rsum(speech, noise, k, q, deltaA);

                if (rSum < 0.15)
                {
                    w[i] = 1.54 * Math.Pow(rSum, 0.25) * (1 - Math.Exp(-11 * rSum));
                }
                else
                {
                    w[i] = 1 - Math.Exp(-11 * rSum / (1 + 0.7 * rSum));
                }

                i++;
            }

            return w;
        }

        public static double Rsum(double[] speech, double[] noise, double[] k, double q, double[] deltaA)
        {
            double[] Q = new double[speech.Length];
            double[] p = new double[speech.Length];
            double[] R = new double[speech.Length];

            for (int i = 0; i < speech.Length; i++)
            {
                Q[i] = speech[i] - noise[i] + q - deltaA[i];

                if (Q[i] <= 0)
                {
                    p[i] = 0.78 + 5.46 * Math.Exp(-4.3 * Math.Pow(10, -3) * Math.Pow(27.3 - Math.Abs(Q[i]), 2));
                }
                else
                {
                    p[i] = 1 - (0.78 + 5.46 * Math.Exp(-4.3 * Math.Pow(10, -3) * Math.Pow(27.3 - Math.Abs(Q[i]), 2)));
                }

                p[i] /= 1 + Math.Pow(10, 0.1 * Math.Abs(Q[i]));

                R[i] = k[i] * p[i];
            }

            return R.Sum();
        }
    }
}
