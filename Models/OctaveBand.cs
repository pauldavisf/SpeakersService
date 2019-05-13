using System;
namespace Models
{
    public class OctaveBand
    {
        public double LowFreq { get; set; } // нижняя и верхняя частота полосы
        public double HighFreq { get; set; } // верхняя
        public double RootFreq { get; set; } // основная частота

        public double SumLevel { get; set; } // сумма уровней
        public double IntegralLevel { get; set; } // интегральный уровень
        public double NormalizedLevel { get; set; } // нормализованный 
    }
}