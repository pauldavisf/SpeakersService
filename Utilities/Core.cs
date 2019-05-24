using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using static Utilities.DefaultParameters;

namespace Utilities
{
    public static class Core
    {
        /// <summary>
        /// Gets the QF or w.
        /// </summary>
        /// <returns>The QF or w.</returns>
        /// <param name="from">диапазон для q</param>
        /// <param name="to">диапазон для q</param>
        /// <param name="neededW">разборчивость, которую мы хотим обеспечить</param>
        public static int GetQForW(int from, int to, double neededW)
        {
            // В качестве Lречь берём усреднённый спектр речи
            var Lspeech = AverageSpeechLevels;

            //А в качестве Lшума формантоподобную помеху
            var Lnoise = FormantNoiseLevels;

            //Рассчитываем как и в баке W в зависимости от разных q С УСРЕДНЕННЫМ СПЕКТРОМ
            for (int q = from; q < to; q++)
            {
                var w = Intelligibility.GetW(Lspeech, Lnoise, k, deltaA, q);
                if (w >= neededW)
                {
                    // прога сказала, что к этому значениию W отлично подходит вот это q. 
                    // то есть выбрало из 60 всего одно.
                    return q;
                }
            }

            throw new ArgumentException("There is no q for given W");
        }

        public static SpeakerViewModel GetWorstSpeaker(List<SpeakerViewModel> speakers)
        {
            var worstSpeaker = new SpeakerViewModel(); // речь худшего диктора, массив из 7 элементов
            var bestW = double.NegativeInfinity; // лучшая разборчивость

            foreach(var speaker in speakers) // идем по всем дикторам
            {
                var w = Intelligibility.GetW(speaker.Speech, FormantNoiseLevels, k, deltaA, 0.0);

                if (w > bestW)
                {
                    bestW = w;
                    worstSpeaker = speaker; // если разборчивость диктора выше остальных, он становится худшим
                }
            }

            return worstSpeaker;
        }

        /// <summary>
        /// Gets the noise levels.
        /// </summary>
        /// <returns>The noise levels.</returns>
        /// <param name="speakers">список всех выбранных дикторов</param>
        /// <param name="from">от скольки брать q (у нас обычно от -30)</param>
        /// <param name="to">до скольки брать q (у нас обычно до 30).</param>
        /// <param name="w">разборчивость, которую хотим обеспечить.</param>
        public static double[] GetNoiseLevels(List<SpeakerViewModel> speakers, int from, int to, double w)
        {
            var Lnoise = new double[speakers.First().Speech.Length];

            // прога сказала, что к этому значениию W отлично подходит вот это q. 
            // то есть выбрало из 60 всего одно.
            var q = GetQForW(from, to, w);

            // Рассчитываем Qi для этого q (всё также с усредненным спектром речи и форм-й помехой)
            // Должны получить по одной Q для каждой полосы, то есть семь штук. Эти Qi теперь для нас эталонные
            var Q = Intelligibility.GetQ(AverageSpeechLevels,
                                         FormantNoiseLevels, 
                                         q,
                                         deltaA);

            // Lречь - наш самый худший диктор!!!! Спектр речи худшего диктора (не усредненный, как брали выше)
            var worstSpeaker = GetWorstSpeaker(speakers);
            var Lspeech = worstSpeaker.Speech;

            // Теперь начинаем считать для дикторов
            for (int i = 0; i < Lspeech.Length; i++)
            {
                Lnoise[i] = Lspeech[i] - deltaA[i] - Q[i];
            }

            return Lnoise;
        }
    }
}
