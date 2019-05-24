using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class SpeakerViewModel
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public double[] Speech { get; set; } // уровни речи для каждой полосы

        public Dictionary<string, double[]> W { get; set; } // словарь "вид шума строкой" -> массив разборчивостей для каждого q
    }
}
