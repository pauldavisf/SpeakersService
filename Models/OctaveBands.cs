using System;
namespace Models
{
    public class OctaveBands
    {
        public OctaveBand[] Bands { get; private set; }
        public int Count { get; private set; }

        public OctaveBands(int bandsCount)
        {
            Count = bandsCount;
            Bands = new OctaveBand[Count];

            for (int i = 0; i < bandsCount; i++)
            {
                Bands[i] = new OctaveBand();
            }
        }

        public OctaveBand this[int index]
        {
            get
            {
                return Bands[index];
            }

            set
            {
                Bands[index] = value;
            }
        }
    }
}
